using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public PlayerController player;
	public Stairway stairway;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			player.RotateLeft();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			player.RotateRight();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			player.Jump();
		}
	}
}
