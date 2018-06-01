using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScript : MonoBehaviour {

    public GameObject lobbyDisplay;
    public GameObject kitchenDisplay;
    public GameObject bedroomDisplay;
    public GameObject bathroomDisplay;
    public GameObject PauseDisplay;
    public GameObject windowsSetting;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;
        
    private bool isPaused;
    public enum room { Lobby,Kitchen,Bedroom,Bathroom };
	// Use this for initialization
	void Start () {
        windowsSetting.SetActive(false);
        PauseDisplay.SetActive(false);
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseDisplay.SetActive(true);
            isPaused = true;
        }
	}
    public void continueGame()
    {
        PauseDisplay.SetActive(false);
        isPaused = false;
    }
    public void changeRoom(room room)
    {
        lobbyDisplay.SetActive(false);
        kitchenDisplay.SetActive(false);
        bedroomDisplay.SetActive(false);
        bathroomDisplay.SetActive(false);
        int x = (int)room;
        switch (x)
        {
            case 0:
                lobbyDisplay.SetActive(true);
                break;
            case 1:
                kitchenDisplay.SetActive(true);
                break;
            case 2:
                bedroomDisplay.SetActive(true);
                break;
            case 3:
                bathroomDisplay.SetActive(true);
                break;
        }
    }
    public void changemusic()
    {
        MusicSource.volume = VolumeMusic.value;
    }

    public void changesound()
    {
        SoundSource.volume = VolumeSound.value;
    }

    public void closesetting(bool applied)
    {
        if (applied)

        {

        }
        windowsSetting.SetActive(false);

    }

    public void opensetting()
    {
        windowsSetting.SetActive(true);
    }

    
}
