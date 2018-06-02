using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject windowsSetting;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;

    private float currentMusicVolume;
    private float currentSoundVolume;
    // Use this for initialization
    void Start()
    {
        //windowsSetting.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

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
            
            playerPrefHandler.applySetting(MusicSource.volume, SoundSource.volume);
            currentMusicVolume = playerPrefHandler.GetMusicVolume();
            currentSoundVolume = playerPrefHandler.GetSoundVolume();
        }
        SoundSource.volume = currentSoundVolume;
        MusicSource.volume = currentMusicVolume;
        windowsSetting.SetActive(false);

    }

    public void opensetting()
    {
        windowsSetting.SetActive(true);
    }

    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
    void setSoundPrefs()
    {
        MusicSource.volume = playerPrefHandler.GetMusicVolume();
        SoundSource.volume = playerPrefHandler.GetSoundVolume();
    }
}