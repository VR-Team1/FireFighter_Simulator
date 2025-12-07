using UnityEngine;

public class Scene4BGMController : MonoBehaviour
{
    [Header("秋扁 家府")]
    public AudioSource helicopterAudio;

    [Header("阂 家府")]
    public AudioSource fireAudio;

    private void Start()
    {
        if (helicopterAudio != null && !helicopterAudio.isPlaying)
        {
            helicopterAudio.loop = true;
            helicopterAudio.Play();
        }

        if (fireAudio != null && !fireAudio.isPlaying)
        {
            fireAudio.loop = true;
            fireAudio.Play();
        }
    }

    public void StopAllBGM()
    {
        if (helicopterAudio != null) helicopterAudio.Stop();
        if (fireAudio != null) fireAudio.Stop();
    }
}
