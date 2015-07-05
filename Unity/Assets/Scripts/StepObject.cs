using UnityEngine;
using System.Collections;

public class StepObject : MonoBehaviour {
	
	public int numberOfParts = 10;
	public Vector3 partSize;
	public float crashingTime = 0.1f;  

	public Stairway delegateStairway;

	private GameObject[] parts;

	private int _crashingIndex = 0;
	// Use this for initialization
	void Start () {
		generateStepParts ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void generateStepParts(){
		parts = new GameObject[numberOfParts];

		for(int index = 0; index < parts.Length ; index++) {
			GameObject part;

			part = GameObject.CreatePrimitive(PrimitiveType.Cube);
			part.transform.parent = transform;
			part.transform.localScale = partSize;

			Rigidbody rb = part.AddComponent<Rigidbody>();
			rb.freezeRotation = true;
		
			initPart(part,index);		
			parts[index] = part;
		}
	}

	public void StartCrashing(){
		_crashingIndex = 0;
		StartCoroutine ("CrashPart");
	}
	IEnumerator CrashPart(){

		while(_crashingIndex < parts.Length){
			yield return new WaitForSeconds(Random.Range(crashingTime*0.8f,crashingTime * 1.2f));

			GameObject part = parts[_crashingIndex++];
			part.GetComponent<Rigidbody>().isKinematic = false;
		}

		stepDone ();
	}
	void initPart(GameObject part, int index){
		part.transform.localPosition = new Vector3(partSize.x * index, 0 ,0 );
		part.GetComponent<Rigidbody>().isKinematic = true;
	}
	void stepDone(){
		for (int index = 0; index < parts.Length; index++) {
			initPart(parts[index],index);
		}
		delegateStairway.ReloadStep (this);
	}
}
