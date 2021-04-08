using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonScript : MonoBehaviour
{
    
    public GameObject volumeOn;
    public GameObject volumeOff;
    public GameObject musicOn;
    public GameObject musicOff;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        if (PlayerPrefs.GetInt("volume", 1) == 1)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
        }
        else
        {
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }
    }


    public void ChangeMusic()
    {
        int music = PlayerPrefs.GetInt("music", 1);
        if (music == 1)
        {
            music = 0;
            PlayerPrefs.SetInt("music", music);
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            music = 1;
            PlayerPrefs.SetInt("music", music);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
    }
    public void ChangeVolume()
    {
        int music = PlayerPrefs.GetInt("volume", 1);
        if (music == 1)
        {
            music = 0;
            PlayerPrefs.SetInt("volume", music);
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }
        else
        {
            music = 1;
            PlayerPrefs.SetInt("volume", music);
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
        }
    }
}
