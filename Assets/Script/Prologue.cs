using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour {
    public Text textArea;
    public string[] strings;
    public float speed = 0.1f;

    int stringIndex = 0;
    int characterIndex = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(DisplayTimer());
	}
    IEnumerator DisplayTimer()
    {
        string temp = null;
        for (int i = 0; i < strings.Length; i++)
        {
            temp += strings[i] + " ";
        }
        while (characterIndex < temp.Length - 1)
        {
            yield return new WaitForSeconds(speed);
                textArea.text = temp.Substring(0, characterIndex);
            characterIndex++;
        }
        
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
