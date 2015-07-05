using UnityEngine;
using System.Collections;

public class PlayerVision : MonoBehaviour {

	public string barrierTagName;
	public float visionDistance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool hasBarrier(){
	
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Ray ray = new Ray(transform.position,fwd);
		RaycastHit hitInfo;	

		if (Physics.Raycast(ray ,out hitInfo, visionDistance )) {
			Debug.Log(hitInfo.collider.gameObject.name);
			if(hitInfo.collider.gameObject.tag == barrierTagName)
				return true;
		}

		return false;
	}
}
