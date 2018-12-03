using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
	
	void Start () {
		if (GameObject.Find ("Glub") == null) {
			name = "Glub";
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
	
	void Update () {
		if (GameObject.FindObjectOfType<Go>() != null) {
			GetComponent<AudioSource>().volume = Mathf.Lerp (GetComponent<AudioSource>().volume, GameObject.FindObjectOfType<Go>().T/4, Time.deltaTime);
		} else if (Application.loadedLevelName == "WinScreenR" || Application.loadedLevelName == "WinScreenB") {
			GetComponent<AudioSource>().volume = 0.25f;
		} else if (Application.loadedLevelName == "Setup") {
			GetComponent<AudioSource>().volume = 0.6f;
		} else {
			GetComponent<AudioSource>().volume = 1;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Application.loadedLevelName == "WinScreenR" || Application.loadedLevelName == "WinScreenB" || Application.loadedLevelName == "Setup" || Application.loadedLevelName == "Start" || Application.loadedLevelName == "Start1") {
				Application.Quit();
			} else {
				Application.LoadLevel ("Setup");
			}
		}
	}
}
