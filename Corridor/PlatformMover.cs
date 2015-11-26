using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent (typeof (Rigidbody))]
public class PlatformMover : MonoBehaviour {

	private RigidbodyFirstPersonController fpc;
	private Rigidbody rigbod;

	private float periodLength; //in seconds
	private bool frozen;
	private float peakDistance;
	private float speedMultiplier;
	private float phaseShift;


	private float pushForce;

	private Vector3 startPosition;

	void Awake(){
		rigbod = GetComponent<Rigidbody> ();
		periodLength = 5f;
		phaseShift = .5f;
		peakDistance = 10f;
		pushForce = .1f;
		startPosition = transform.position;
	}

	//make the platform move in sinusoidal motion
	void Update(){
		Oscillate ();
	}

	//make the platform move in sinusoidal motion
	void Oscillate(){
		speedMultiplier = Mathf.Sin (2f * Mathf.PI * (Time.realtimeSinceStartup / periodLength + phaseShift));
		rigbod.velocity = Vector3.forward * speedMultiplier * peakDistance;
	}

	//I gave the player gameobject a new "layer", layer 8
	//every time she collides, provide this script with the player's script!
	void OnCollisionEnter(Collision col){
		if (col.gameObject.layer == 8) {
			fpc = col.gameObject.GetComponent<RigidbodyFirstPersonController> ();
		}
	}


	void OnCollisionStay(Collision col){
		if (col.gameObject.layer == 8) {
			//if player is ON TOP and not trying to move
			if (Vector3.Distance(col.contacts[0].point,transform.position) < transform.localScale.x && col.contacts[0].point.y > transform.position.y && !frozen && CrossPlatformInputManager.GetAxis("Horizontal")==0 && CrossPlatformInputManager.GetAxis("Vertical")==0 && !CrossPlatformInputManager.GetButtonDown("Jump")){
				Freeze(col);
			}
			else{ //if player is trying to move
				MovePlayerWithPlatform(col);
			}
		}
	}

	void MovePlayerWithPlatform(Collision col){

		//set the initial velocity of the player as she starts moving to the same velocity as the platform
		if (frozen) {
			frozen = false;
			col.collider.attachedRigidbody.velocity = rigbod.velocity;
		}

		float platformSpeed = rigbod.velocity.z;
		float playerSpeed = col.collider.attachedRigidbody.velocity.z;

		//see the RigidBodyFirstPersonController script for this
		//tell the RigidBodyFPC script how fast the platform is moving
		//Add an immediately corrective force to account for the additional speed the player should be getting
		if (fpc.moving) {
			fpc.velocityOfTrain = new Vector3 (Mathf.Abs (rigbod.velocity.x),Mathf.Abs (rigbod.velocity.y),Mathf.Abs (rigbod.velocity.z));
			col.collider.attachedRigidbody.AddForce (Vector3.forward * Mathf.Abs (platformSpeed) * Mathf.Sign (playerSpeed) * pushForce, ForceMode.VelocityChange);
		}
	}

	//parent and freeze the player
	void Freeze(Collision col){
		frozen = true;
		col.transform.parent = transform;
		col.collider.attachedRigidbody.isKinematic =true;
	}

	//detach the player
	void OnCollisionExit(Collision col){
		if (col.gameObject.layer == 8) {
			transform.DetachChildren();
			fpc.velocityOfTrain = Vector3.zero;
			frozen = false;
		}
	}
}