using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public GameObject windowsSetting;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;


	// Use this for initialization
	void Start () {
        //windowsSetting.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changemusic()
    {
        MusicSource.volume =VolumeMusic.value ;
    }

    public void changesound()
    {
        SoundSource.volume = VolumeSound.value;
    }
}
