using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
	// Use this for initialization
	void Awake () {
		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}
    public void Start()
    {     
        if(SceneManager.GetActiveScene().name!="Menu")
        {
            if(PlayerPrefs.GetInt("music", 1) == 1)
            {
                string name = "Theme";
                Sound s = Array.Find(sounds, sound => sound.name == name);
                s.source.Play();
            }
        }
    }
    public void Play(string name)
    {
        if(PlayerPrefs.GetInt("volume", 1) == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
    }
}
