using UnityEngine;
using System.Collections;

public class Stairway : MonoBehaviour {

	public int numberOfSteps = 10;
	public float stepCrashTime = 2.0f;
	public float partsCrashingTime = 0.3f;
	public int numberOfStepParts = 8;
	public Vector3 partSize;

	public ObstaclePool obstcalePool;

	private StepObject[] stepObjects; 
	private int _crashingStepIndex = 0;
	private int _lastStep;
	private bool _isCrashing;


	// Use this for initialization
	void Start () {

		generateSteps ();

		//StartCoroutine ("StartStepCrashign");

		_lastStep = numberOfSteps;
		Invoke("startCrashingNext", stepCrashTime);
		Invoke ("GenerateInitialObstcales", stepCrashTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void generateSteps(){
		stepObjects = new StepObject[numberOfSteps];
			
		for(int index = 0; index < stepObjects.Length ; index++) {

			GameObject step = new GameObject();
	
			StepObject stepObject = step.AddComponent<StepObject>();
			stepObject.numberOfParts = numberOfStepParts;
			stepObject.crashingTime = partsCrashingTime;
			stepObject.partSize = partSize;

			setStepPosition(stepObject , index);

			stepObject.name = "stepNumber" + index.ToString();
			stepObject.delegateStairway = this;

			stepObjects[index] = stepObject;
		}
	}
	void setStepPosition(StepObject stepObject , int index){
		stepObject.transform.position = new Vector3( 0, index * stepObject.partSize.y, stepObject.partSize.z * index );
	}
	IEnumerator StartStepCrashign(){

		while (true) {

			yield return new WaitForSeconds (stepCrashTime);
			startCrashingNext();
		
		}
	}
	public void GenerateInitialObstcales(){
		for (int i = 0; i < stepObjects.Length; i++) {
			generateObstcalesAtStepIndex(i);
		}
	}
	public void startCrashingNext(){

		if(_crashingStepIndex >= numberOfSteps) _crashingStepIndex = 0;
		stepObjects [_crashingStepIndex++].StartCrashing ();
	
	}
	public void ReloadStep(StepObject stepObject){
	
		setStepPosition (stepObject, _lastStep++);
		startCrashingNext ();
		generateObstcalesAtStepIndex(_lastStep - 1);
	}

	public void generateObstcalesAtStepIndex(int index){

		if (index == 9)
			return;

		int count = Random.Range ((int)0, (int)(numberOfStepParts / 3));

		for (int i = 0; i < count; i++) {

			GameObject obj = obstcalePool.getObject();

			if(obj == null ) continue;

			int posOnX = Random.Range((int) i * numberOfStepParts / count, (int) ((i + 1)* numberOfStepParts / count)) ;

			obj.transform.position = new Vector3(posOnX, index * partSize.y + 0.1f, index );
		}
	}
}
