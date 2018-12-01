using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Go : MonoBehaviour {
	
	public GameObject Objs;
	public Text text;
	
	float T;
	
	void Update () {
		text.text = ""+(((int)-T)+3);
		if (T >= 4) {
			Objs.SetActive(true);
			this.gameObject.SetActive(false);
		}
		T += Time.deltaTime;
	}
}
