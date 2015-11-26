using UnityEngine;
using System.Collections;

public class InitializeLookAtObjective : MonoBehaviour {

	void Awake(){
		Transform objectiveTransform = GameObject.Find ("End Platform").transform;
		transform.LookAt (objectiveTransform);
	}
}
