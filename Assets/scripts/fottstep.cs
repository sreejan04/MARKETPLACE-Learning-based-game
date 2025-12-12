using UnityEngine;

public class Footstep : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    private CharacterController characterController;

    [SerializeField] private AudioClip walkingFootstepClip;
    [SerializeField] private AudioClip runningFootstepClip;
    [SerializeField] private AudioClip jumpClip;

    void Start()
    {
        footstepAudioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();

        if (footstepAudioSource == null)
        {
            Debug.LogError("Footstep AudioSource component missing.");
        }
        if (characterController == null)
        {
            Debug.LogError("CharacterController component missing.");
        }
    }

    // Called by walking animation event
    public void PlayerFootstepSoundWalk()
    {
        if (IsPlayerWalking())
        {
            PlayFootstepSound(walkingFootstepClip);
            Debug.Log("Playing walking footstep sound");
        }
    }

    // Called by running animation event
    public void PlayerFootstepSoundRun()
    {
        if (IsPlayerRunning())
        {
            PlayFootstepSound(runningFootstepClip);
            Debug.Log("Playing running footstep sound");
        }
    }

    // Called by jump animation event
    public void PlayerJumpSound()
    {
        if (footstepAudioSource != null && jumpClip != null)
        {
            footstepAudioSource.PlayOneShot(jumpClip);
            Debug.Log("Playing jump sound");
        }
    }

    private bool IsPlayerWalking()
    {
        if (characterController == null) return false;
        float speed = characterController.velocity.magnitude;
        // Adjust threshold for walking speed range
        return speed > 0.1f && speed < 3.0f;
    }

    private bool IsPlayerRunning()
    {
        if (characterController == null) return false;
        float speed = characterController.velocity.magnitude;
        // Adjust threshold for running speed range
        return speed >= 3.0f;
    }

    private void PlayFootstepSound(AudioClip clip)
    {
        if (clip == null || footstepAudioSource == null) return;

        footstepAudioSource.clip = clip;
        footstepAudioSource.Play();
    }
}
