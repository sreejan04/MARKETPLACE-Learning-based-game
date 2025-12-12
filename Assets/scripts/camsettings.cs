using Controller;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject[] characterPrefabs;  // Assign your character prefabs here in Inspector
    private GameObject playerInstance;

    void Start()
    {
        int index = PlayerPrefs.GetInt("CharacterSelected", 0);

        if (index >= 0 && index < characterPrefabs.Length)
        {
            // Instantiate chosen character at desired position, e.g., Vector3.zero
            playerInstance = Instantiate(characterPrefabs[index], Vector3.zero, Quaternion.identity);

            // Assign player reference to camera script
            ThirdPersonCamera cameraScript = Camera.main.GetComponent<ThirdPersonCamera>();
            if (cameraScript != null)
            {
                // Assuming your ThirdPersonCamera class has a way to set the player transform (e.g., m_Player)
                // If m_Player is private, add a public method or property to assign it:
                cameraScript.SetPlayerTransform(playerInstance.transform);
            }
            else
            {
                Debug.LogWarning("ThirdPersonCamera script not found on main camera.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid character index in PlayerPrefs.");
        }
    }
}
