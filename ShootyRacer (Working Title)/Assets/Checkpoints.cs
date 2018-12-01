using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {
	
	Collider[] points;
	
	public int Checks = 0;
	
	public int Laps;
	
	void Start () {
		Array.Resize(ref points, 5);
		points[0] = GameObject.Find ("Checkpoints/ (0)").GetComponent<Collider>();
		points[1] = GameObject.Find ("Checkpoints/ (1)").GetComponent<Collider>();
		points[2] = GameObject.Find ("Checkpoints/ (2)").GetComponent<Collider>();
		points[3] = GameObject.Find ("Checkpoints/ (3)").GetComponent<Collider>();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other == points[Checks+1]) {
			if (other.transform.InverseTransformPoint(transform.position).z < 0) {
				++Checks;
			}
		}
		if (other == points[0]) {
			if (Checks == 3) {
				++Laps;
				Checks = 0;
			}
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other == points[Checks]) {
			if (other.transform.InverseTransformPoint(transform.position).z < 0) {
				--Checks;
			}
		}
	}
}
