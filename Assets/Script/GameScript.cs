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
    public GameObject debugDisplay;
    public GameObject doneList;
    public Text[] doneAreas;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;
    public GameObject dialougePrefabs;
    //Randomly generated per gameplay
    private bool[] areaDone;
    private int[] gameAreaOrder;
    private int[] areaKey;

    /*
     * Area Index:
     * 0= Kitchen
     * 1= Bedroom
     * 2= Bathroom
     * 
     * 
     * areaDone, finished area
     * gameAreaOrder, order for each gameplay. contains 0,1 and 2. 0=kitchen,1=bedroom,2=bathroom
     * areaKey, place of which the keys are hidden. Index of areaKey corespondents with Area Index
     * i.e areaKey[0]= Kitchen
     */

    private bool isPaused;
    private float currentMusicVolume;
    private float currentSoundVolume;
    private bool debugMode = false;

    


    //public enum room { Lobby,Kitchen,Bedroom,Bathroom };
	// Use this for initialization
	void Start () {
        playerPrefHandler.startNewGame();
        setupGame();

        if (debugMode)
        {
            debugDisplay.SetActive(true);
            debugDisplay.GetComponentInChildren<Text>().text = "This is debug Mode\n" +
                "Area order: "+GetAreaName(gameAreaOrder[0])+"->"+ GetAreaName(gameAreaOrder[1]) 
                + "->"+ GetAreaName(gameAreaOrder[2])+"\nKey per area: Kitchen= "+areaKey[0]
                +"Bedroom= "+areaKey[1]+"Bathroom= "+areaKey[2];
        }
        else
        {
            windowsSetting.SetActive(false);
            PauseDisplay.SetActive(false);
            isPaused = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)&!isPaused)
        {
            PauseDisplay.SetActive(true);

            isPaused = true;
        }
        if (Input.GetKeyDown(KeyCode.O) & !isPaused)
        {
            checkDoneList();
            doneList.SetActive(!doneList.activeSelf);
        }
	}
    void checkDoneList()
    {
        bool[] done = playerPrefHandler.GetAreaDone();
        for (int i = 0; i < 3; i++)
        {
            if (done[i])
            {
                doneAreas[i].text = "Done";
                doneAreas[i].color = Color.green;
            }
            else
            {
                doneAreas[i].text = "Not Done";
                doneAreas[i].color = Color.grey;
            }
        }
    }
    public void continueGame()
    {
        PauseDisplay.SetActive(false);
        isPaused = false;
    }
    private string GetAreaName(int area)
    {
        switch (area)
        {
            case 0:
                return "Kitchen";
            case 1:
                return "Bedroom";
            case 2:
                return "Bathroom";
            default:
                return "Area not found";
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
        currentMusicVolume = MusicSource.volume;
        currentSoundVolume = SoundSource.volume;
    }
    void setSoundPrefs()
    {
        MusicSource.volume = playerPrefHandler.GetMusicVolume();
        SoundSource.volume = playerPrefHandler.GetSoundVolume();
    }
    void setupGame()
    {
        areaDone = playerPrefHandler.GetAreaDone();
        gameAreaOrder = playerPrefHandler.GetAreaGame();
        areaKey = playerPrefHandler.GetAreaKeyList();
    }
    public void changeRoom(int room)
        /*index
         * less than 0=lobby
         * 0=kitchen
         * 1=bedroom
         * 2=bathroom
         */

    {
        if (checkRoom(room))
        {
            lobbyDisplay.SetActive(false);
            kitchenDisplay.SetActive(false);
            bedroomDisplay.SetActive(false);
            bathroomDisplay.SetActive(false);

            switch (room)
            {
                
                case 0:
                    kitchenDisplay.SetActive(true);
                    break;
                case 1:
                    bedroomDisplay.SetActive(true);
                    break;
                case 2:
                    bathroomDisplay.SetActive(true);
                    break;
                default:
                    lobbyDisplay.SetActive(true);
                    break;
            }
        }
        else
        {
            Debug.Log("Door has not been unlocked");
        }
    }
    bool checkRoom(int room)
    {
        if (room < 0)
        {
            return true;
        }
        int areaOrderNumber=-1;
        for (int i = 0; i < gameAreaOrder.Length; i++)
        {
            if (room == gameAreaOrder[i]) {
                areaOrderNumber = i;
                break; }
        }
        if (areaOrderNumber == 0)
        {
            return true;
        }
        else
        {
            return areaDone[areaOrderNumber - 1];
        }
    }
}
