using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class StoryControl : MonoBehaviourPunCallbacks
{
    public AudioSource MusicAudioSource;
    public AudioSource ClickButtonAudioSource;
    public AudioClip ClickSound;

    void Start()
    {
        AddBackgroundMusicVolume();
    }

    void AddBackgroundMusicVolume()
    {
        if (MusicAudioSource.volume < 1)
        {
            MusicAudioSource.volume += 0.02f;
            Invoke("AddBackgroundMusicVolume", 0.05f);
        }
        else
        {
            CancelInvoke("AddBackgroundMusicVolume");
        }
    }

    public void OnClickSkipStory()
    {
        ClickButtonAudioSource.PlayOneShot(ClickSound);
        Invoke("ChangeToGameScene", 0.5f);
    }

    void ChangeToGameScene()
    {
        MusicAudioSource.volume = 0;
        SceneManager.LoadScene("Start Scene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartControl.IsDisconnected = true;
        SceneManager.LoadScene("Start Scene");
    }
}
