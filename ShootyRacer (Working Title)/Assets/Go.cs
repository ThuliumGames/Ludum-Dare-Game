using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Go : MonoBehaviour {
	
	public GameObject Objs;
	public Text text;
	
	public float T;
	
	void Update () {
		if ((int)T >= 3) {
			text.fontSize = 400;
			text.text = "GO!";
		} else {
			text.text = ""+(((int)-T)+3);
		}
		if (T >= 4) {
			Objs.SetActive(true);
			this.gameObject.SetActive(false);
		}
		T += Time.deltaTime;
		
		T = Mathf.Clamp (T, 0, 4);
	}
}
