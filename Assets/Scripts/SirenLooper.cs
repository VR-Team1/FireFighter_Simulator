using UnityEngine;

public class SirenLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public float loopStart = 5f; 
    public float loopEnd = 18f;  

    private bool isPlaying = false;

    public void PlaySirenLoop()
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogWarning("AudioSource 또는 Clip이 없음");
            return;
        }

        audioSource.time = loopStart;
        audioSource.Play();
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (audioSource.time >= loopEnd)
        {
            audioSource.time = loopStart;
        }
    }
}
