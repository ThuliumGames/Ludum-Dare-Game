using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	bool Turning;
	
	float Heat = 0;
	float MaxHeat = 2000;
	Image HeatGage;
	
	void Start () {
		HtDst = 4;
		StPos = transform.position;
		RB = GetComponent<Rigidbody>();
		RB.centerOfMass = new Vector3 (0, 0, 1.25f);
		St = GameObject.Find("Player"+Player+"StatsGlub").GetComponent<Stats>();
		Acceleration = St.Accs[St.Stts[0]];
		MaxSpeed = St.Spds[St.Stts[1]];
		RotSpeed = St.Trns[St.Stts[2]];
		if (Player == 1) {
			HeatGage = GameObject.Find("HR").GetComponent<Image>();
		} else {
			HeatGage = GameObject.Find("HB").GetComponent<Image>();
		}
	}
	
	void Update () {
		
		if (Player == 1 || (Player == 2 && Stats.NumPlayers == 2)) {
			
			if (Heat < MaxHeat || St.Stts[3] == 1) {
				
				MaxHeat = 2000;
				RaycastHit Hit;
				if (!Physics.Raycast (transform.position, transform.forward*Mathf.Clamp(Speed, -1, 1), out Hit, 5, LM)) {
						
						Speed += (Input.GetAxis ("Vertical" + Player)/2) * Time.deltaTime * Acceleration;
						if (Mathf.Abs (Input.GetAxis ("Vertical" + Player)) < 0.1f || (Input.GetAxis ("Vertical" + Player)>0&&Speed<0) || (Input.GetAxis ("Vertical" + Player)<0&&Speed>0)) {
							if (St.Stts[3] == 0) {
								Speed /= 1.05f;
							}
						}
						Speed = Mathf.Clamp (Speed, -MaxSpeed/2, MaxSpeed);
						
						Vector3 GoForce = transform.forward * Speed;
						RB.velocity = new Vector3 (GoForce.x, RB.velocity.y, GoForce.z);
						RB.angularVelocity = transform.up * Input.GetAxis ("Horizontal" + Player) * (RB.velocity.magnitude) * RotSpeed;
				} else {
					Speed = -5*Mathf.Clamp(Speed, -1, 1);
				}
				
				Heat += (Mathf.Abs(Speed)-25) * 2 * Time.deltaTime;
			
			} else {
				MaxHeat = 1;
				Heat -= 100 * Time.deltaTime;
				Speed /= 2f;
				RB.angularVelocity /= 2f;
			}
			
		} else {
			
			//AI
			if (Heat < MaxHeat || St.Stts[3] == 1) {
				
				MaxHeat = 2000;
				RaycastHit HitDown;
				Physics.Raycast (transform.position, -transform.up, out HitDown, Mathf.Infinity, LM);
				
				RaycastHit Hit;
				if (!Physics.Raycast (transform.position, transform.forward, out Hit, HtDst, LM) && !Turning) {
					
					HtDst = 4;
					Speed += (0.5f) * Time.deltaTime * Acceleration;
					
				} else {
					Turning = true;
					if (HtDst == 4) {
						Speed = -5*Mathf.Clamp(Speed, -1, 1);
					}
					HtDst = 40/(St.Stts[2]+1);
					Speed += (-0.5f) * Time.deltaTime * Acceleration;
					if (St.Stts[3] == 0) {
						Speed /= 1.05f;
					}
					
				}
				
				if (Mathf.Abs((GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x) < 0.5f || Physics.Raycast (transform.position, -transform.forward, HtDst, LM)) {
					Turning = false;
				}
				
				Speed = Mathf.Clamp (Speed, -MaxSpeed/1.5f, MaxSpeed);
				
				Vector3 GoForce = transform.forward * Speed;
				RB.velocity = new Vector3 (GoForce.x, RB.velocity.y, GoForce.z);
				
				if (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).z > -1) {
					if (HtDst > 4) {
						RB.angularVelocity = transform.up * (Mathf.Clamp (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x, -1, 1)) * (RB.velocity.magnitude) * RotSpeed*2;
					} else {
						RB.angularVelocity = transform.up * (Mathf.Clamp (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x/4f, -1, 1)) * (RB.velocity.magnitude) * RotSpeed;
					}
				} else {
					RB.angularVelocity = transform.up * (Mathf.Clamp (transform.InverseTransformPoint(GameObject.Find(HitDown.collider.gameObject.GetComponentInParent<AutoConnect>().gameObject.name+"/EndPoint").transform.position).x, -1, 1)) * (RB.velocity.magnitude) * RotSpeed;
				}
				
				Heat += (Mathf.Abs(Speed)-25) * 2 * Time.deltaTime;
				
			} else {
				MaxHeat = 1;
				Heat -= 100 * Time.deltaTime;
				Speed /= 2f;
				RB.angularVelocity /= 2f;
			}
		}
		if (St.Stts[3] == 0) {
			Heat = Mathf.Clamp (Heat, 0, 2000);
			HeatGage.GetComponent<RectTransform>().localScale = new Vector3 (Heat, 1, 1);
		} else {
			HeatGage.GetComponent<RectTransform>().localScale = new Vector3 (0, 1, 1);
		}
		if (MaxHeat < 1000) {
			HeatGage.color = Color.red;
		} else {
			HeatGage.color = Color.green;
		}
	}
}