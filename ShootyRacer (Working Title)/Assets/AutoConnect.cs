using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class AutoConnect : MonoBehaviour {
	
	void Update () {
		
		if (transform.position != new Vector3 (0, 0.01f, 0)) {
			float Dist = 1000;
			GameObject T = null;
			foreach (AutoConnect G in GameObject.FindObjectsOfType<AutoConnect>()) {
				if (Vector3.Distance (transform.position, GameObject.Find(G.gameObject.name+"/EndPoint").transform.position) < Dist && G != this) {
					T = GameObject.Find(G.gameObject.name+"/EndPoint");
					Dist = Vector3.Distance (transform.position, GameObject.Find(G.gameObject.name+"/EndPoint").transform.position);
				}
			}
			transform.position = T.transform.position;
		}
		
		this.enabled = false;
	}
}
