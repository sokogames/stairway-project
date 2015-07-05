using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public const string LEFT = "left";
	public const string RIGHT = "right";
	public const string FORWARD = "forward";
	public const string BACK = "back";

	private string[] directions;

	private int _directionID;
	public int directionID {
		get{
			return _directionID; 
		}

		set{
			_directionID = value > 3 ? 0 : value < 0 ? 3 : value;	
		}
	}
	// Use this for initialization
	public float rotationSpeed = 0.1f;
	public float correctionSize = 3.0f;
	public float jumpHeight = 4.0f;
	public float jumpDistance = 1.3f;
	public float jumpMaxDistance = 1.0f;
	public PlayerVision playerVision;

	private Quaternion toRotation;
	private float angle;
	private Vector3 jumpStartPos;

	void Start () {
		directionID = 0;
		directions = new string[4]{FORWARD, RIGHT, BACK, LEFT};
		toRotation = transform.rotation;
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Debug.DrawRay(transform.position,fwd);

		if (Quaternion.Angle (transform.rotation, toRotation) > 1) {
			transform.rotation = Quaternion.Slerp (transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
		}
	}

	void FixedUpdate(){
		
		correctPosition ();

		if (Quaternion.Angle (transform.rotation, toRotation) < 5) {
			transform.rotation = toRotation;
		}
	}

	public void RotateLeft(){
		angle = -90;
		directionID--;
		Rotate();
	}
	public void RotateRight(){
		angle = 90;
		directionID++;
		Rotate ();
	}
	private void Rotate(){
	
		transform.rotation = toRotation;
		toRotation = toRotation * Quaternion.AngleAxis(angle, Vector3.up);

	}
	public void Jump(){

		if (playerVision.hasBarrier ())
			return;

		if (GetComponent<Rigidbody>().velocity.magnitude > 0.01f) return;

		jumpStartPos = transform.position;

		float jumpX = 0, jumpZ = 0;

		switch (directions [directionID]) {
			case LEFT: jumpX = -jumpDistance; jumpZ = 0; break;
			case RIGHT: jumpX = jumpDistance; jumpZ = 0; break;
			case FORWARD: jumpX = 0; jumpZ = jumpDistance; break;
			case BACK: jumpX = 0; jumpZ = -jumpDistance; break;
		}
															
		GetComponent<Rigidbody>().velocity = new Vector3(jumpX,jumpHeight,jumpZ);
	}

	void correctPosition(){

		Vector3 velocity = GetComponent<Rigidbody> ().velocity;
		Vector3 temp = transform.position;

		if (Mathf.Abs (jumpStartPos.x - transform.position.x) > jumpMaxDistance) {
			velocity.x = 0.0f;
		}

		if (Mathf.Abs (jumpStartPos.z - transform.position.z) > jumpMaxDistance) {
			velocity.z = 0.0f;
		}

		GetComponent<Rigidbody> ().velocity = velocity;

		if (velocity.magnitude < 0.1f) {
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y , Mathf.RoundToInt(transform.position.z));
		}


/*
		if (velocity.magnitude < 0.1f && velocity.magnitude != 0) 
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}*/
	}
}
