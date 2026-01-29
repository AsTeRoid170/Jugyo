using UnityEngine;

public class PlayerAudio: MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource footstepSource;
    public AudioSource actionSource;

    [Header("AudioClip")]
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public AudioClip landClip;

    public void PlayFootstep()
    {
        if (!footstepSource.isPlaying)
        {
            footstepSource.clip = footstepClip;
            footstepSource.Play();
        }
    }

    public void StopFootstep()
    {
        footstepSource.Stop();
    }

    public void PlayJump()
    {
        actionSource.PlayOneShot(jumpClip);
    }

    public void PlayLand()
    {
        actionSource.PlayOneShot(landClip);
    }
}
