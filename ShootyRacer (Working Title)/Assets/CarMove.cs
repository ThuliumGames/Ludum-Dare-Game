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
	
	float HtDst = 4;
	
	void Start () {
		HtDst = 4;
		StPos = transform.position;
		RB = GetComponent<Rigidbody>();
		RB.centerOfMass = new Vector3 (0, 0, 1.25f);
		St = GameObject.Find("Player"+Player+"Stats").GetComponent<Stats>();
		Acceleration = St.Accs[St.Stts[0]];
		MaxSpeed = St.Spds[St.Stts[1]];
		RotSpeed = St.Trns[St.Stts[2]];
	}
	
	void Update () {
		
		if (Player == 1 || (Player == 2 && Stats.NumPlayers == 2)) {
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
				Speed = -1;
			}
		} else {
			RaycastHit HitDown;
			Physics.Raycast (transform.position, -transform.up, out HitDown, Mathf.Infinity, LM);
			RaycastHit Hit;
			if (!Physics.Raycast (transform.position, transform.forward, out Hit, HtDst, LM)) {
				HtDst = 4;
				Speed += (0.5f) * Time.deltaTime * Acceleration;
			} else {
				
				RB.angularVelocity = transform.up * (Mathf.Clamp (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x, -1, 1)) * (RB.velocity.magnitude) * RotSpeed;
				
				HtDst = 20;
				Speed += (-0.5f) * Time.deltaTime * Acceleration;
				Speed /= 1.05f;
			}
			Speed = Mathf.Clamp (Speed, -MaxSpeed/2, MaxSpeed);
			
			Vector3 GoForce = transform.forward * Speed;
			RB.velocity = new Vector3 (GoForce.x, RB.velocity.y, GoForce.z);
			
			if (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).z > -1) {
				RB.angularVelocity = transform.up * (Mathf.Clamp (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x/4f, -1, 1)) * (RB.velocity.magnitude) * RotSpeed;
			} else {
				RB.angularVelocity = transform.up * (RB.velocity.magnitude) * RotSpeed;
			}
		}
	}
}