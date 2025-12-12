using UnityEngine;

public class NPCinteractable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactionUI;
    public void Interact()
    {
        void Start()
        {
            // Ensure UI is hidden at the beginning of the game as fallback
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
        if (interactionUI != null)
        {
            interactionUI.SetActive(true); // Show the UI when interacting
        }
        else
        {
            Debug.LogWarning("Interaction UI is not assigned.");
        }
    }
}
