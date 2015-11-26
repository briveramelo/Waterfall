using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ObjectiveCamera : MonoBehaviour {

	public Transform playerCameraTransform;
	public RigidbodyFirstPersonController fpc;
	public Transform endPlatformTransform;
	public bool fast;
	private Rigidbody rigbod;
	private float moveSpeed;
	private float slowSpeed;
	private float fastSpeed;
	private Camera thisCam;
	private bool looking;
	private float minRotationSpeed;
	private float maxRotationSpeed;
	private float rotationSpeedMultiplier;
	private float rotationSpeed;

	void Awake(){
		slowSpeed = 6f;
		fastSpeed = 15f;
		minRotationSpeed = 2f;
		maxRotationSpeed = 10f;
		rotationSpeedMultiplier = 4f;
		thisCam = GetComponent<Camera> ();
		thisCam.enabled = true;

		endPlatformTransform = GameObject.Find("End Platform").transform;
		fpc = GameObject.FindObjectOfType<RigidbodyFirstPersonController> ();
		playerCameraTransform = fpc.transform.GetChild (0);
		fpc.enabled = false;

		StartCoroutine ( EnableCameras (false));
		StartCoroutine ( Move ());
	}

	public IEnumerator Move(){
		float distanceAway = 10f;
		moveSpeed = fast ? fastSpeed : slowSpeed;
		while (distanceAway > 0.005f) {
			distanceAway = Vector3.Distance(transform.position, playerCameraTransform.position);
			transform.position = Vector3.MoveTowards(transform.position,playerCameraTransform.position, Time.deltaTime * moveSpeed);
			if (looking){
				transform.LookAt(endPlatformTransform);
			}
			else{
				rotationSpeed = Mathf.Clamp(rotationSpeedMultiplier / distanceAway,minRotationSpeed,maxRotationSpeed);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation(endPlatformTransform.position-transform.position,-Physics.gravity),rotationSpeed);
				if (Quaternion.Angle(transform.rotation,Quaternion.LookRotation(endPlatformTransform.position-transform.position,-Physics.gravity))<.1f){
					looking = true;
				}
			}
			yield return null;
		}
		yield return StartCoroutine (EnableCameras (true));
		Destroy (gameObject, .1f);
		fpc.enabled = true;
	}

	public IEnumerator EnableCameras(bool enable){
		foreach (Camera cam in FindObjectsOfType<Camera>()) {
			if (cam != thisCam) {
				cam.enabled = enable;
			}
		}
		yield return null;
	}
}
