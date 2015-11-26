using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Teleportal : MonoBehaviour {

	private Transform otherTeleportal;
	private Transform otherTeleportalRotation;
	public AudioSource teleportalNoise;
	private MouseLook mouseLook;

	void Awake(){
		otherTeleportal = name == "TeleportalA" ? transform.parent.GetChild(1) : transform.parent.GetChild(0);
		otherTeleportalRotation = otherTeleportal.GetChild (0);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 8) {
			Teleport(col);
		}
	}

	void Teleport(Collider col){
		col.GetComponent<RigidbodyFirstPersonController> ().mouseLook.SetRotation (otherTeleportalRotation.rotation);
		col.transform.rotation = otherTeleportalRotation.rotation;
		col.transform.GetChild(0).rotation = otherTeleportalRotation.rotation;
		col.transform.position = otherTeleportalRotation.position + otherTeleportalRotation.forward * 2;
		teleportalNoise.Play();
	}


}
