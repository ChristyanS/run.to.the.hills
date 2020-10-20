using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public AudioSource audio;
    public void Pause()
    {
        Time.timeScale = 0;
        audio.enabled = false;
    }

    public void Play()
    {
        Time.timeScale = 1;
        audio.enabled = true;
    }
}