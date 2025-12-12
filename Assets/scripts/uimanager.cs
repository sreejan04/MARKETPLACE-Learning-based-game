using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QnAController : MonoBehaviour
{
    public TMP_InputField answerInput;
    public Button submitButton;
    public Button phoneAFriendButton;
    public TMP_Text hintOrSolutionText;
    public TMP_Text starCountText;
    public GameObject panelToClose;

    [Header("Correct Answer Panel")]
    public GameObject correctAnswerPanel;
    public TMP_Text correctAnswerText;
    public AudioSource correctAnswerAudioSource;

    [Header("Phone a Friend")]
    public GameObject phoneAFriendPanel;
    public TMP_Text phoneAFriendText;
    public Button backButton;
    public AudioSource phoneAFriendAudioSource; // 🔊 New Audio for phone-a-friend panel

    private string correctAnswer = "2";
    private string hint = "Hint: Think of it as subtraction: “Money you gave” − “Cost.” You can also count up from 8 to 10.";
    private string solution = "You gave ten rupees for a mango that costs eight. Subtracting eight from ten leaves two. Another way is to count up: from eight to nine is one rupee, and from nine to ten is another. So, you will get two rupees back as change.";

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
        phoneAFriendButton.onClick.AddListener(OnPhoneAFriend);
        backButton.onClick.AddListener(ClosePhoneAFriendPanel);

        UpdateStarCount();
        hintOrSolutionText.text = "";

        if (correctAnswerPanel != null)
            correctAnswerPanel.SetActive(false);

        if (phoneAFriendPanel != null)
            phoneAFriendPanel.SetActive(false);  // Hide phone-a-friend panel at start
    }

    void OnSubmit()
    {
        if (answerInput.text.ToLower() == correctAnswer.ToLower())
        {
            StarManager.Instance.AddStar();
            UpdateStarCount();
            hintOrSolutionText.text = "";
            answerInput.text = "";

            if (panelToClose != null)
                panelToClose.SetActive(false);

            StartCoroutine(ShowCorrectAnswerPanelRoutine());
        }
        else
        {
            hintOrSolutionText.text = hint;
            if (correctAnswerPanel != null)
                correctAnswerPanel.SetActive(false);
        }
    }

    void OnPhoneAFriend()
    {
        if (phoneAFriendPanel != null)
        {
            phoneAFriendPanel.SetActive(true);
            if (phoneAFriendText != null)
                phoneAFriendText.text = solution;

            // 🎵 Play audio when panel opens
            if (phoneAFriendAudioSource != null)
                phoneAFriendAudioSource.Play();
        }
    }

    void ClosePhoneAFriendPanel()
    {
        if (phoneAFriendPanel != null)
            phoneAFriendPanel.SetActive(false);

        // ⏹ Stop audio when panel closes
        if (phoneAFriendAudioSource != null)
            phoneAFriendAudioSource.Stop();
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
}
