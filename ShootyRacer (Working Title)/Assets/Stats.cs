using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
	
	float[] Accs = {10, 50, 500};
	float[] Spds = {10, 50, 250};
	float[] Trns = {0.01f, 0.1f, 0.75f};
	bool SeatBeltOrBreaks;
	
	public int[] Stts = {0, 2, 1, 0};
	public bool[] changed = {false, false, false};
	
	public Dropdown[] Choices;
	
	int[] PrevStats = {0, 2, 1, 0};
	
	void Update () {
		
		for (int i = 0; i < 4; ++i) {
			if (i < 3) {
				changed[i] = false;
			}
			Stts[i] = Choices[i].value;
			print (Stts[i] + " : " + PrevStats[i]);
			if (Stts[i] != PrevStats[i]) {
				if (i < 3) {
					changed[i] = true;
				}
			}
		}
		if (changed[0] == true) {
			if (Stts[1] == Stts[0]) {
				Choices[1].value = PrevStats[0];
			}
			if (Stts[2] == Stts[0]) {
				Choices[2].value = PrevStats[0];
			}
		}
		if (changed[1] == true) {
			if (Stts[0] == Stts[1]) {
				Choices[0].value = PrevStats[1];
			}
			if (Stts[2] == Stts[1]) {
				Choices[2].value = PrevStats[1];
			}
		}
		if (changed[2] == true) {
			if (Stts[1] == Stts[2]) {
				Choices[1].value = PrevStats[2];
			}
			if (Stts[0] == Stts[2]) {
				Choices[0].value = PrevStats[2];
			}
		}
		
		PrevStats = Stts;
	}
}
