using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {
    public Text textArea;
    public string[] strings;
    public string[] stringsEN;
    public float speed = 0.1f;
    public GameObject continueButton;
    int stringIndex = 0;
    int characterIndex = 0;

    bool isIndonesian = false;
	// Use this for initialization
	void Start () {
        continueButton.SetActive(false);
        StartCoroutine(DisplayTimer());
	}
    IEnumerator DisplayTimer()
    {
        string temp = null;
        for (int i = 0; i < strings.Length; i++)
        {
            if (isIndonesian)
            {
                temp += strings[i] + " ";
            }
            else
            {
                temp += stringsEN[i] + " ";
            }
            
        }
        while (characterIndex < temp.Length)
        {
            yield return new WaitForSeconds(speed);
                textArea.text = temp.Substring(0, characterIndex);
            characterIndex++;
        }
        continueButton.SetActive(true);
    }
    
	// Update is called once per frame
	void Update () {
		
	}
    public void continuePressed()
    {
        SceneManager.LoadScene(1);
    }
}
