using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclePool : MonoBehaviour {

	public GameObject[] ObjectPrefab;
	public int poolSize;
	public int currentSize;
	public Stairway delegateStairway;

	private List<GameObject> list;
	// Use this for initialization
	void Start () {

		list = new List<GameObject>();
		for(int i = 0 ; i < poolSize; i++){
			GameObject obj = (GameObject)Instantiate(ObjectPrefab[Random.Range(0,10) % 2]);
			list.Add(obj);
			obj.SetActive(false);
			obj.GetComponent<DeactivateOnInvisible>().delegateStairway = delegateStairway;
		}
	}
	
	public GameObject getObject(){
		if(list.Count > 0){ 
			GameObject obj = list[0];
			list.RemoveAt(0);
			obj.SetActive(true);
			return obj;
		}
		return null;
	}
	public void DestroyObject(GameObject obj){
		list.Add(obj);
		obj.SetActive(false);
	}
	public void ClearPool(){
		for(int i = list.Count - 1; i > 0; i--){
			GameObject obj = list[i];
			list.RemoveAt(i);
			Destroy(obj);
		}
		list = null;
	}
}
