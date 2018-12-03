using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
	
	public float[] Accs = {10, 50, 500};
	public float[] Spds = {75, 150, 300};
	public float[] Trns = {0.0625f, 0.125f, 0.25f};
	
	public int[] Stts = {0, 2, 1, 0};
	
	public Dropdown[] Choices;
	
	public int[] PrevStats = {0, 2, 1, 0};
	
	public static int NumPlayers = 2;
	
	Transform[] allObjects;
	
	void Start () {
		if (GameObject.Find(name+"Glub") == null) {
			DontDestroyOnLoad(this.gameObject);
			name = name+"Glub";
		} else {
			Destroy (this.gameObject);
		}
		allObjects = GetComponentInChildren<Canvas>().GetComponentsInChildren<Transform>();
	}
	
	void Update () {
		
		if (Application.loadedLevelName != "Setup") {
			foreach (Transform C in allObjects) {
				C.gameObject.SetActive(false);
			}
		} else {
			foreach (Transform C in allObjects) {
				C.gameObject.SetActive(true);
			}
		}
		
		NumPlayers = Choices[4].value+1;
		
		for (int i = 0; i < 4; ++i) {
			Stts[i] = Choices[i].value;
			if (Stts[i] != PrevStats[i]) {
				if (i < 3) {
					if (i == 0) {
						if (Stts[1] == Stts[0]) {
							Choices[1].value = PrevStats[0];
							Stts[1] = PrevStats[0];
						}
						if (Stts[2] == Stts[0]) {
							Choices[2].value = PrevStats[0];
							Stts[2] = PrevStats[0];
						}
					}
					if (i == 1) {
						if (Stts[0] == Stts[1]) {
							Choices[0].value = PrevStats[1];
							Stts[0] = PrevStats[1];
						}
						if (Stts[2] == Stts[1]) {
							Choices[2].value = PrevStats[1];
							Stts[2] = PrevStats[1];
						}
					}
					if (i == 2) {
						if (Stts[1] == Stts[2]) {
							Choices[1].value = PrevStats[2];
							Stts[1] = PrevStats[2];
						}
						if (Stts[0] == Stts[2]) {
							Choices[0].value = PrevStats[2];
							Stts[0] = PrevStats[2];
						}
					}
				}
				PrevStats[i] = Stts[i];
			}
		}
	}
	
	void Sta () {
		int Lev = Random.Range (1, 16);
		Application.LoadLevel ("Level" + Lev);
	}
}
