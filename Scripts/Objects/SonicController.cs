using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicController : MonoBehaviour {
	public bool grounded;
	public Animator anim;
	public Rigidbody rb;
	public GameObject forward;
	public bool jump;
	public bool homingAttack;
	public GameObject rotOBJ;
	public bool inHouse;
	public bool dbgJump;
	public bool homTarg;
	public Vector3 rayPos;

	public AudioClip jumpClip;
	public AudioClip homingClip;

	void Start () {
		grounded = true;
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		anim.SetFloat ("MoveSpeed", (Mathf.Lerp (anim.GetFloat ("MoveSpeed"), rb.velocity.magnitude, Time.deltaTime * 7f)));
		anim.SetBool ("Jumping", jump);

		Vector2 inputPower = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		if (grounded == false) {
			if (homTarg == false) {
				rb.AddForce (-Vector3.up * 45f);
				rb.AddForce (transform.forward * inputPower.magnitude * 35f);
			}
		}
	}

	void Update () {
		if (homingAttack == true) {
			GetComponentInChildren<TrailRenderer> ().enabled = true;
		} else {
			homTarg = false;
			GetComponentInChildren<TrailRenderer> ().enabled = false;
		}

		if (jump == false)
			homingAttack = false;
		
		if (Input.GetButtonDown ("Jump")) {
			if (grounded == true) {
				if (jump == false) {
					GetComponent<AudioSource> ().clip = jumpClip;
					GetComponent<AudioSource> ().Play ();
					jump = true;
					rb.AddForce (transform.up * 1605f);
				}
			} else {
				if (jump == true) {
					if (homingAttack == false) {
						homingAttack = true;
						GetComponent<AudioSource> ().clip = homingClip;
						GetComponent<AudioSource> ().Play ();
						Target[] homingTarget = FindObjectsOfType<Target> ();
						int idx2 = 0;
						float distance = 999;
						Vector3 targetPos = Vector3.zero;
						Vector3 targetWorldPos = Vector3.zero;
						bool foundTarg = false;
						while (idx2 < homingTarget.Length) {
							if ((homingTarget [idx2].transform.position - transform.position).magnitude < distance) {
								distance = (homingTarget [idx2].transform.position - transform.position).magnitude;
								targetPos = (homingTarget [idx2].transform.position - transform.position);
								targetWorldPos = homingTarget [idx2].transform.position;
								if (distance < 25) {
									foundTarg = true;
								}
							}
							idx2++;
						}
						if (foundTarg) {
							homTarg = true;
							rb.useGravity = false;
							if (grounded == false) {
								homingAttack = true;
							}
							rb.velocity = targetPos.normalized * 65;
						} else {
							homTarg = false;
							rb.velocity = transform.forward * 65;
							rb.useGravity = true;

						}
					}
				}
				if (dbgJump == true) {
					jump = true;
					rb.AddForce (transform.up * 1605f);
				}
			}
		}

		RaycastHit hit;
		Vector3 pos = transform.position + transform.up * 0.1f;
		rayPos = pos;
		if (Physics.Raycast (pos, -transform.up, out hit, 0.3f)) {


			transform.rotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
		
			if (jump == false) {
				if (Vector3.Dot (transform.forward, rb.velocity) > 30f) {
					transform.position = new Vector3 (transform.position.x, hit.point.y, transform.position.z);
				}
			}
		}

		RaycastHit[] cast = Physics.RaycastAll (transform.position + transform.up * 0.6f, -transform.up, 0.8f);
		int idx = 0;
		grounded = false;
		while (idx < cast.Length) {
			if (cast [idx].collider.gameObject.tag == "Floor") {
				grounded = true;
				rb.useGravity = true;
			}
			idx++;
		}

		float myAngle = Mathf.Atan2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")) * Mathf.Rad2Deg;

		float bodyRotation = myAngle + forward.transform.localEulerAngles.y;

		Vector2 inputPower = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		Quaternion direction = Quaternion.FromToRotation (transform.up, Vector3.up) * transform.rotation;

		if (inputPower.magnitude > 0.1) {
			transform.Rotate (new Vector3 (0, Mathf.DeltaAngle (direction.eulerAngles.y, bodyRotation), 0), Space.Self);
		}

		float vel;
		if (inHouse == true) {
			vel = Mathf.Lerp (Vector3.Dot (transform.forward, rb.velocity), inputPower.magnitude * 10f, Time.deltaTime * 3f);
		} else {
			vel = Mathf.Lerp (Vector3.Dot (transform.forward, rb.velocity), inputPower.magnitude * 175f, Time.deltaTime * 0.3f);
		}

		Quaternion originalRot = transform.rotation;
		float xVal = Mathf.Lerp (originalRot.x, 0, Time.deltaTime * 15f);
		float zVal = Mathf.Lerp (originalRot.z, 0, Time.deltaTime * 15f);

		if (grounded == true) {
			if (jump == false) {
				if (inputPower.magnitude > 0.3f) {
					rb.velocity = transform.forward * vel;
				} else {
					if (vel < 25)
						rb.velocity = rb.velocity * 0.95f;
				}
			}

		} else {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, Vector3.up) * transform.rotation, Time.deltaTime * 10f);
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.tag == "Floor")
			jump = false;
		if (collision.tag == "Item") {
			if (jump == true) {
				rb.useGravity = true;
				rb.velocity = Vector3.up * 25f;
				homingAttack = false;
			}
		}
		if (collision.tag == "Enemy") {
			if (jump == true) {
				rb.useGravity = true;
				rb.velocity = Vector3.up * 35f;
				homingAttack = false;
			}
		}
	}

	void OnCollisionEnter (Collision col) {
		rb.useGravity = true;
	}
}
