using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
	
	float T = 0;
	
	void Update () {
		
		if (T > 2.5f) {
			int i = Random.Range (0, 100);
			
			if (i == 50) {
				Application.LoadLevel("Start1");
			} else {
				Application.LoadLevel("Start");
			}
		}
		
		T += Time.deltaTime;
	}
}
