using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {
	
	Rigidbody RB;
	
	float Speed;
	public float Acceleration = 50;
	public float MaxSpeed = 50;
	public float RotSpeed = 0.1f;
	
	public Vector3 Pos;
	
	public LayerMask LM;
	
	public int Player;
	
	void Start () {
		RB = GetComponent<Rigidbody>();
		RB.centerOfMass = new Vector3 (0, 0, 1.25f);
	}
	
	void Update () {
		
		RaycastHit Hit;
		if (!Physics.Raycast (transform.position, transform.forward, out Hit, 1, LM)) {
				Speed += (Input.GetAxis ("Vertical" + Player)-0.5f) * Time.deltaTime * Acceleration;
				
				if (Input.GetAxis ("Vertical" + Player) >= 0) {
					Speed = Mathf.Clamp (Speed, 0, MaxSpeed);
				} else {
					Speed = Mathf.Clamp (Speed, -MaxSpeed/2, MaxSpeed);
				}
				Vector3 GoForce = transform.forward * Speed;
				RB.velocity = new Vector3 (GoForce.x, RB.velocity.y, GoForce.z);
				RB.angularVelocity = transform.up * Input.GetAxis ("Horizontal" + Player) * (RB.velocity.magnitude * (Input.GetAxis ("Vertical" + Player)/2)) * RotSpeed;
		} else {
			Speed = -1;
		}
	}
}