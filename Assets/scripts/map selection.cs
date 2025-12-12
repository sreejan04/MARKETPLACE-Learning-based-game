using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // ✅ Import TextMeshPro

public class MapSelection : MonoBehaviour
{
    [System.Serializable]
    public class MapData
    {
        public string mapName;
        public Sprite mapImage;
        public string sceneName;
    }

    [Header("UI References")]
    [SerializeField] private Image mapDisplay;                // Map image
    [SerializeField] private TextMeshProUGUI mapTitle;        // ✅ TextMeshPro for title
    [SerializeField] private Button playButton;               // Play button

    [Header("Maps")]
    [SerializeField] private MapData[] maps;

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.3f;
    [SerializeField] private float slideOffset = 500f; // distance of slide in px

    private int currentIndex = 0;
    private bool isSliding = false;

    void Start()
    {
        if (maps.Length > 0)
            UpdateUIInstant();

        playButton.onClick.AddListener(PlayMap);
    }

    public void NextMap()
    {
        if (isSliding) return;
        int nextIndex = (currentIndex + 1) % maps.Length;
        StartCoroutine(SlideToMap(nextIndex, true));
    }

    public void PreviousMap()
    {
        if (isSliding) return;
        int prevIndex = (currentIndex - 1 + maps.Length) % maps.Length;
        StartCoroutine(SlideToMap(prevIndex, false));
    }

    private IEnumerator SlideToMap(int newIndex, bool toRight)
    {
        isSliding = true;

        // Create new image for incoming map
        GameObject newObj = new GameObject("TempMap");
        Image newImage = newObj.AddComponent<Image>();
        newObj.transform.SetParent(mapDisplay.transform.parent, false);
        newImage.sprite = maps[newIndex].mapImage;
        newImage.rectTransform.sizeDelta = mapDisplay.rectTransform.sizeDelta;
        newImage.rectTransform.pivot = mapDisplay.rectTransform.pivot;

        // Position new image off-screen
        float dir = toRight ? 1f : -1f;
        newImage.rectTransform.anchoredPosition = new Vector2(dir * slideOffset, 0);

        // Current & new rects
        RectTransform oldRect = mapDisplay.rectTransform;
        RectTransform newRect = newImage.rectTransform;

        float elapsed = 0;
        Vector2 startOld = oldRect.anchoredPosition;
        Vector2 startNew = newRect.anchoredPosition;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);

            oldRect.anchoredPosition = Vector2.Lerp(startOld, new Vector2(-dir * slideOffset, 0), t);
            newRect.anchoredPosition = Vector2.Lerp(startNew, Vector2.zero, t);

            yield return null;
        }

        // Apply new data to main display
        mapDisplay.sprite = maps[newIndex].mapImage;
        if (mapTitle != null)
            mapTitle.text = maps[newIndex].mapName;

        // Reset & cleanup
        mapDisplay.rectTransform.anchoredPosition = Vector2.zero;
        Destroy(newObj.gameObject);

        currentIndex = newIndex;
        isSliding = false;
    }

    private void UpdateUIInstant()
    {
        mapDisplay.sprite = maps[currentIndex].mapImage;
        if (mapTitle != null)
            mapTitle.text = maps[currentIndex].mapName;
    }

    private void PlayMap()
    {
        string sceneToLoad = maps[currentIndex].sceneName;
        SceneManager.LoadScene(sceneToLoad);
    }
}
