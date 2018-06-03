using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class playerPrefHandler
{


    /*
     * PlayerPref guide
     * 
     * PlayerPrefs float musicVolume, music volume throughout the game
     * PlayerPrefs float soundVolume, sound volume throughout the game
     * 
     * PlayerPrefsX int[] areaGame, order of area for the gamethrough. 0=kitchen,1=bedroom,2=bathroom
     * PlayerPrefs int area1Key, location of key in area 1. value= 0-*
     * PlayerPrefs int area2Key, location of key in area 2. value= 0-*
     * PlayerPrefs int area3Key, location of key in area 3. value= 0-*
     * 
     * PlayerPrefsX bool[] areaDone, list of completed area. 1 true can go to area2, 2 trues can go to area3, 3 trues you can exit
     * 
     * PlayerPrefs bool saved, check if there's any saved game. a finished game will turn saved to false
     * PlayerPrefs bool gameFinish, if true, change screen from Game to Main Menu will automatically enable Credit part
     */

    public static void applySetting(float musicVolume, float soundVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        
    }
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume", 1);
    }
    public static float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume", 1);
    }
    public static void startNewGame()
    {

        int[] area = { 0, 1, 2 };
        int rand = (int)Random.Range(0, 2);
        int temp;
        for (int j = 0; j < 10; j++)
        {

            for (int i = 2; i > 0; i--)
            {
                rand = (int)Random.Range(0, 2);
                temp = area[rand];
                area[rand] = area[i];
                area[i] = temp;
            }

        }
        int area1 = (int)Random.Range((int)1, (int)11);
        int area2 = (int)Random.Range((int)1, (int)11);
        int area3 = (int)Random.Range((int)1, (int)11);
        bool[] doneArea = { false, false, false };

        PlayerPrefsX.SetIntArray("areaGame", area);
        PlayerPrefs.SetInt("area1Key", area1);
        PlayerPrefs.SetInt("area2Key", area2);
        PlayerPrefs.SetInt("area3Key", area3);
        PlayerPrefsX.SetBoolArray("areaDone", doneArea);
        PlayerPrefsX.SetBool("saved", false);
    }
    public static int[] GetAreaGame()
    {
        return PlayerPrefsX.GetIntArray("areaGame");
    }
    public static void SetAreaDone(bool[] done)
    {
        PlayerPrefsX.SetBoolArray("areaDone", done);
        PlayerPrefsX.SetBool("saved", true);
    }
    public static bool[] GetAreaDone()
    {
        return PlayerPrefsX.GetBoolArray("areaDone");
    }
    public static int[] GetAreaKeyList()
    {
        int[] list = { PlayerPrefs.GetInt("area1Key"), PlayerPrefs.GetInt("area2Key"), PlayerPrefs.GetInt("area3Key") };
        return list;
    }
    public static bool isSaved()
    {
        return PlayerPrefsX.GetBool("saved", false);
    }
    public static void endGame()
    {
        PlayerPrefsX.SetBool("saved", false);
        PlayerPrefsX.SetBool("GameFinish", true);
    }
    public static bool GetIt()
    {
        return PlayerPrefsX.GetBool("GameFinish", false);
    }
    public static void finishIt()
    {
        PlayerPrefsX.SetBool("GameFinish", false);
    }

}
