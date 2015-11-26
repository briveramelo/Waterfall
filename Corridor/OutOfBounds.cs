using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class OutOfBounds : MonoBehaviour {

	public CheckPointManager checkPointManager;
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 8) {
			StartCoroutine(checkPointManager.ResetPosition(col.gameObject));
		}
	}
}
