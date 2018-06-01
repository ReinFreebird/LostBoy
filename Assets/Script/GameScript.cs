using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public GameObject lobbyDisplay;
    public GameObject kitchenDisplay;
    public GameObject bedroomDisplay;
    public GameObject bathroomDisplay;

    public enum room { Lobby,Kitchen,Bedroom,Bathroom };
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
