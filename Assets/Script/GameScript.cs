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
    public Text currentRoomText;
    public Text[] doneAreas;
    public Slider VolumeMusic;
    public Slider VolumeSound;
    public AudioSource MusicSource;
    public AudioSource SoundSource;
    public GameObject dialougeBox;
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
    private int currentArea;

    


    //public enum room { Lobby,Kitchen,Bedroom,Bathroom };
	// Use this for initialization
	void Start () {
        playerPrefHandler.startNewGame();
        setSoundPrefs();
        setupGame();
        currentArea = -1;
        currentRoomText.text = GetAreaName(currentArea);
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
    void openDialouge(string line)
    {
        dialougeBox.GetComponentInChildren<Text>().text = line;
        dialougeBox.SetActive(true);
        isPaused = true;
    }
    public void closeDialouge()
    {
        dialougeBox.SetActive(false);
        isPaused = false;
    }
    void checkDoneList()
    {
        bool[] done = areaDone;
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
                return "Lobby";
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
        setSoundPrefs();
    }
    void setSoundPrefs()
    {
        MusicSource.volume = playerPrefHandler.GetMusicVolume();
        SoundSource.volume = playerPrefHandler.GetSoundVolume();
        VolumeMusic.value = playerPrefHandler.GetMusicVolume();
        VolumeSound.value = playerPrefHandler.GetSoundVolume();
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
        checkDoneList();
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
                    currentArea = 0;
                    break;
                case 1:
                    bedroomDisplay.SetActive(true);
                    currentArea = 1;
                    break;
                case 2:
                    bathroomDisplay.SetActive(true);
                    currentArea = 2;
                    break;
                default:
                    lobbyDisplay.SetActive(true);
                    currentArea = -1;
                    break;
            }
            currentRoomText.text = GetAreaName(currentArea);
        }
        else
        {
            openDialouge("Door has not been unlocked");
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
    public void checkItem(int index)
    {
        //Get area number
        int areaToCheck = 0;
        for (int i = 0; i < 3; i++)
        {
            if (gameAreaOrder[i] == currentArea)
            {
                areaToCheck = i;
                break;
            }
        }
        if (areaDone[areaToCheck])
        {
            openDialouge("You have already found the key in this room");

        }
        else {
            int rightRooms = 0;
            if (index == areaKey[currentArea])
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!areaDone[i])
                    {
                        rightRooms = i;
                        areaDone[i] = true;
                        break;
                    }
                }
                if (areaDone[2])
                {
                    openDialouge("You have found to exit the house");
                }
                else {
                    openDialouge("You have found the key to go to " + GetAreaName(gameAreaOrder[rightRooms + 1]));
                }
                
            }
            else
            {
                openDialouge("You found nothing");
            }
        }
    }
    public void saveGame()
    {
        playerPrefHandler.SetAreaDone(areaDone);
    }
    public void finishGame()
    {
        if (areaDone[2])
        {
            openDialouge("YOU WON");
        }
        else
        {
            openDialouge("You don't have the key to exit");
        }
    }
}
