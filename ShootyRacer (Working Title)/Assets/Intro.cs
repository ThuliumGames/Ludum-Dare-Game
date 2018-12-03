using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
	
	float T = 0;
	int i;
	
	void Start () {
		i = Random.Range (0, 101);
	}
	
	void Update () {
		
		if (T > 2.5f) {
			
			if (i == 50) {
				Application.LoadLevel("Start1");
			} else {
				Application.LoadLevel("Start");
			}
		}
		
		T += Time.deltaTime;
	}
}
