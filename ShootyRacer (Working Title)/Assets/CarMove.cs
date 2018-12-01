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
	
	Stats St;
	
	public Vector3 StPos;
	
	void Start () {
		StPos = transform.position;
		RB = GetComponent<Rigidbody>();
		RB.centerOfMass = new Vector3 (0, 0, 1.25f);
		St = GameObject.Find("Player"+Player+"Stats").GetComponent<Stats>();
		Acceleration = St.Accs[St.Stts[0]];
		MaxSpeed = St.Spds[St.Stts[1]];
		RotSpeed = St.Trns[St.Stts[2]];
	}
	
	void Update () {
		
		RaycastHit Hit;
		if (!Physics.Raycast (transform.position, transform.forward, out Hit, 1, LM)) {
				
				Speed += (Input.GetAxis ("Vertical" + Player)/2) * Time.deltaTime * Acceleration;
				
				if (Mathf.Abs (Input.GetAxis ("Vertical" + Player)) < 0.1f || (Input.GetAxis ("Vertical" + Player)>0&&Speed<0) || (Input.GetAxis ("Vertical" + Player)<0&&Speed>0)) {
					Speed /= 1.05f;
				}
				Speed = Mathf.Clamp (Speed, -MaxSpeed/2, MaxSpeed);
				
				Vector3 GoForce = transform.forward * Speed;
				RB.velocity = new Vector3 (GoForce.x, RB.velocity.y, GoForce.z);
				RB.angularVelocity = transform.up * Input.GetAxis ("Horizontal" + Player) * (RB.velocity.magnitude) * RotSpeed;
		} else {
			if (St.Stts[3] == 1) {
				transform.position = StPos;
				--GetComponent<Checkpoints>().Laps;
			}
			Speed = -1;
		}
	}
}