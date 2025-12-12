using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QnAControllerr : MonoBehaviour
{
    public TMP_Dropdown answerDropdown;
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
    public GameObject phoneAFriendPanel;      // 🔹 Panel for phone-a-friend
    public TMP_Text phoneAFriendText;         // 🔹 Text inside the panel
    public Button backButton;                 // 🔹 Back button to close panel
    public AudioSource phoneAFriendAudioSource; // 🔹 Audio source for hint sound

    private string correctAnswer = "5 short";
    private string hint = "Hint: First find the total cost of ten sweets, then compare it with the money you have.";
    private string solution = "Each sweet costs three rupees, so ten sweets cost thirty. With only twenty-five rupees, you are five short. If you buy as many as possible, you can get eight sweets for twenty-four rupees and will have one rupee left. So, you are five short";

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
            phoneAFriendPanel.SetActive(false);  // hide phone-a-friend at start
    }

    void OnSubmit()
    {
        string selectedAnswer = answerDropdown.options[answerDropdown.value].text;

        if (selectedAnswer.ToLower() == correctAnswer.ToLower())
        {
            StarManager.Instance.AddStar();
            UpdateStarCount();
            hintOrSolutionText.text = "";

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

            // 🔊 Play audio when opening
            if (phoneAFriendAudioSource != null)
                phoneAFriendAudioSource.Play();
        }
    }

    void ClosePhoneAFriendPanel()
    {
        if (phoneAFriendPanel != null)
            phoneAFriendPanel.SetActive(false);

        // ⏹ Stop audio when closing
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
