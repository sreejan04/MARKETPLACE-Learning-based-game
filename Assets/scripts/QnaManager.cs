using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MoneyQnaController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject notePrefab;   // Rs 10 note
    public GameObject coinPrefab;   // Rs 1 coin

    [Header("Spawn Settings")]
    public Transform spawnParent;   // Parent transform where money spawns
    public Transform dropArea;      // Drop area reference
    public int spawnExtra = 5;      // Extra money to confuse user

    [Header("UI References")]
    public Button submitButton;
    public Button solutionButton;
    public Button startAnsweringButton;
    public TMP_Text startAnsweringButtonText;
    public TMP_Text hintOrSolutionText;
    public TMP_Text starCountText;
    public GameObject panelToClose;
    public GameObject correctAnswerPanel;
    public TMP_Text correctAnswerText;
    public AudioSource correctAnswerAudioSource;
    public GameObject QnaUIPanel;

    [Header("Solution Panel")]              // 🔹 New
    public GameObject solutionPanel;        // panel that shows solution
    public TMP_Text solutionPanelText;      // solution text inside panel
    public Button solutionBackButton;       // back button for solution panel
    public AudioSource solutionAudioSource; // audio for solution panel

    private List<GameObject> spawnedMoney = new List<GameObject>();

    // Correct answer values
    private int correctNotes = 2;
    private int correctCoins = 4;

    private string hint = "Try grouping two 10 rupee notes and then add coins to reach the total.";
    private string solution = "Each egg costs two rupees. For twelve eggs, multiply two by twelve. Two times twelve equals twenty-four. So, the total cost is twenty-four rupees, which means you will need to pay twenty-four rupees to buy twelve eggs.";

    private bool isResetState = false;

    void Start()
    {
        QnaUIPanel.SetActive(false);

        submitButton.onClick.AddListener(OnSubmit);
        solutionButton.onClick.AddListener(OnSolution);  // now opens panel
        startAnsweringButton.onClick.AddListener(OnStartAnsweringButtonClick);

        if (solutionBackButton != null)
            solutionBackButton.onClick.AddListener(CloseSolutionPanel);

        if (correctAnswerPanel != null)
            correctAnswerPanel.SetActive(false);

        if (solutionPanel != null)
            solutionPanel.SetActive(false); // hide at start

        UpdateStarCount();

        if (startAnsweringButtonText != null)
            startAnsweringButtonText.text = "START ANSWERING";
    }

    public void EnableQnaUI()
    {
        QnaUIPanel.SetActive(true);
        SpawnMoneyPrefabs();
        hintOrSolutionText.text = "";

        if (startAnsweringButtonText != null)
        {
            startAnsweringButtonText.text = "START ANSWERING";
            isResetState = false;
        }
    }

    void SpawnMoneyPrefabs()
    {
        foreach (GameObject go in spawnedMoney) Destroy(go);
        spawnedMoney.Clear();

        int totalNotes = correctNotes + spawnExtra;
        for (int i = 0; i < totalNotes; i++)
        {
            GameObject note = Instantiate(notePrefab, spawnParent);
            spawnedMoney.Add(note);
        }

        int totalCoins = correctCoins + spawnExtra;
        for (int i = 0; i < totalCoins; i++)
        {
            GameObject coin = Instantiate(coinPrefab, spawnParent);
            spawnedMoney.Add(coin);
        }
    }

    void OnSubmit()
    {
        int noteCount = 0;
        int coinCount = 0;

        foreach (Transform child in dropArea)
        {
            if (child.CompareTag("Note")) noteCount++;
            if (child.CompareTag("Coin")) coinCount++;
        }

        if (noteCount == correctNotes && coinCount == correctCoins)
        {
            StarManager.Instance.AddStar();
            UpdateStarCount();
            hintOrSolutionText.text = "";

            if (panelToClose != null) panelToClose.SetActive(false);

            StartCoroutine(ShowCorrectAnswerPanelRoutine());

            if (startAnsweringButtonText != null)
            {
                startAnsweringButtonText.text = "START ANSWERING";
                isResetState = false;
            }
        }
        else
        {
            hintOrSolutionText.text = hint;

            foreach (GameObject go in spawnedMoney)
            {
                if (go != null)
                {
                    var handler = go.GetComponent<DragDropHandler>();
                    if (handler != null) handler.ResetPosition();
                }
            }

            if (correctAnswerPanel != null)
                correctAnswerPanel.SetActive(false);

            if (startAnsweringButtonText != null)
            {
                startAnsweringButtonText.text = "RESET";
                isResetState = true;
            }
        }
    }

    void OnSolution()
    {
        if (solutionPanel != null)
        {
            solutionPanel.SetActive(true);
            if (solutionPanelText != null)
                solutionPanelText.text = solution;

            if (solutionAudioSource != null)
                solutionAudioSource.Play();
        }
    }

    void CloseSolutionPanel()
    {
        if (solutionPanel != null)
            solutionPanel.SetActive(false);

        if (solutionAudioSource != null)
            solutionAudioSource.Stop();
    }

    void UpdateStarCount()
    {
        starCountText.text = StarManager.Instance.StarCount.ToString();
    }

    IEnumerator ShowCorrectAnswerPanelRoutine()
    {
        if (correctAnswerPanel != null && correctAnswerText != null)
        {
            correctAnswerText.text = "Correct! You earned a star.";
            correctAnswerPanel.SetActive(true);
            if (correctAnswerAudioSource != null)
                correctAnswerAudioSource.Play();
            yield return new WaitForSeconds(3f);
            correctAnswerPanel.SetActive(false);
            if (correctAnswerAudioSource != null)
                correctAnswerAudioSource.Stop();
        }
    }

    void OnStartAnsweringButtonClick()
    {
        if (isResetState)
        {
            if (startAnsweringButtonText != null)
            {
                startAnsweringButtonText.text = "START ANSWERING";
                isResetState = false;
            }

            ClearDropArea();
            SpawnMoneyPrefabs();
            hintOrSolutionText.text = "";
        }
        else
        {
            EnableQnaUI();
        }
    }

    void ClearDropArea()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in dropArea)
        {
            children.Add(child);
        }
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
}
