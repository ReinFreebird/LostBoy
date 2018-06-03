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
    public AudioClip[] SoundClips;
    /*click button choice 2
     * ketemu key item 2
     * Tidak ketemu cancel 2
     * Apply Setting Jingle 2
     * Buka kertas Book pag 1
     * buka pintu open 1
     * menang short triumphal
     */


    private float currentMusicVolume;
    private float currentSoundVolume;
    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(1028, 769, true);
    }
    void Start()
    {
        currentMusicVolume = playerPrefHandler.GetMusicVolume();
        currentSoundVolume = playerPrefHandler.GetSoundVolume();
        setSoundPrefs();
        
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
        setSoundPrefs();
        Sound(4);
    }

    public void StartGame()
    {
        playerPrefHandler.startNewGame();
        SceneManager.LoadScene(2);
    }
    public void Load()
    {
        if (!playerPrefHandler.isSaved())
        {
            StartGame();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    void setSoundPrefs()
    {
        MusicSource.volume = playerPrefHandler.GetMusicVolume();
        SoundSource.volume = playerPrefHandler.GetSoundVolume();
        VolumeMusic.value = playerPrefHandler.GetMusicVolume();
        VolumeSound.value = playerPrefHandler.GetSoundVolume();

    }

    public void Sound(int sounds)
    {
    SoundSource.clip = SoundClips[sounds];
        SoundSource.Play();

    }
    public void quit()
    {
        //Debug.Log (123);
        Sound(1);
        Application.Quit();
        
    }
}