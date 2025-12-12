using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance { get; private set; }
    public int StarCount { get; private set; } = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddStar()
    {
        StarCount++;
    }
}
