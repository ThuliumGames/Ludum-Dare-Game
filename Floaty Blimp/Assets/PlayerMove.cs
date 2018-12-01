using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public Rigidbody RB;
	
	public float Speed = 10;
	public float RotSpeed = 10;
	
	public Transform Cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RB.velocity = (transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * Speed)+(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * Speed);
		RB.angularVelocity = transform.up * Input.GetAxis("Mouse X") * Time.deltaTime * RotSpeed;
		
		Cam.localEulerAngles -= new Vector3 (Input.GetAxis("Mouse Y")*2, 0, 0);
		if (Cam.localEulerAngles.x < 180) {
			Cam.localEulerAngles = new Vector3 (Mathf.Clamp (Cam.localEulerAngles.x, -1, 85), 0, 0);
		} else {
			Cam.localEulerAngles = new Vector3 (Mathf.Clamp (Cam.localEulerAngles.x, 275, 361), 0, 0);
		}
	}
}
