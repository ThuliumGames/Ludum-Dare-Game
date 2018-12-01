using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {
	
	public Transform[] Cars;
	
	void Update () {
		transform.position = (Cars[0].position+Cars[1].position)/2;
		transform.Translate (0, 0, -100);
		GetComponent<Camera>().orthographicSize = Vector3.Distance(Cars[0].position, Cars[1].position);
		GetComponent<Camera>().orthographicSize = Mathf.Clamp (GetComponent<Camera>().orthographicSize, 40, Mathf.Infinity);
	}
}
