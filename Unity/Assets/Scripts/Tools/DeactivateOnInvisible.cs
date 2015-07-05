using UnityEngine;
using System.Collections;

public class DeactivateOnInvisible : MonoBehaviour {

	public Stairway delegateStairway; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible() {
		delegateStairway.obstcalePool.DestroyObject (gameObject);
	}
}
