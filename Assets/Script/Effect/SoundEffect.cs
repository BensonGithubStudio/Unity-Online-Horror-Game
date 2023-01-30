using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource SoundAudioSource;
    public float Time1;
    public float Time2;

    // Start is called before the first frame update
    void Start()
    {
        SoundAudioSource = this.gameObject.GetComponent<AudioSource>();

        float a = Random.Range(Time1, Time2);
        Invoke("PlaySound", a);
    }

    void PlaySound()
    {
        SoundAudioSource.volume = StartControl.SoundVolume;
        SoundAudioSource.Play();

        float a = Random.Range(Time1, Time2);
        Invoke("PlaySound", a);
    }
}
