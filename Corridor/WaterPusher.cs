using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WaterPusher : MonoBehaviour {

	public float pushForceWith;
	public float moveDirY;
	public bool pushing;

	void Awake(){
		pushForceWith = 55f;
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.layer == 8) {
			Push(col.GetComponent<Rigidbody>());
		}
	}

	void Push(Rigidbody rigbod){
		if (rigbod.velocity.y < 10f) {
			rigbod.AddForce(Vector3.up * pushForceWith);
		}
	}

}
