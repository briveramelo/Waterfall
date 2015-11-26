using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public CheckPointManager checkPointManager;
	public int checkPointNumber;
	public bool set;
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 8 && !set) {
			StartCoroutine(SetCheckpoint());
		}
	}

	public IEnumerator SetCheckpoint(){
		set = true;
		if (checkPointNumber > checkPointManager.currentCheckPoint) {
			checkPointManager.currentCheckPoint = checkPointNumber;
		}
		yield return null;
	}
}