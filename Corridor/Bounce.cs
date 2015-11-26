using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Bounce : MonoBehaviour {

	public float baseBounceForce;
	public float speedMultiplier;
	public float maxBounceForce;
	public float playerSpeed;
	public float bounceForce;
	public bool delay;
	public bool doIT;
	public Rigidbody playerBody1;

	void Awake(){
		baseBounceForce = 100f;
		speedMultiplier = 4.7f;
		maxBounceForce = 180f;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 8 && col.GetComponent<Rigidbody>() && !delay) {
			StartCoroutine (Hit(col.GetComponent<Rigidbody>()));
		}
	}

	public IEnumerator Hit(Rigidbody playerBody){
		delay = true;
		playerSpeed = Mathf.Abs (playerBody.velocity.y);
		bounceForce = Mathf.Clamp((baseBounceForce + speedMultiplier * playerSpeed), baseBounceForce, maxBounceForce);

		playerBody.velocity = new Vector3 (playerBody.velocity.x, 0f, playerBody.velocity.z); //neutralize vertical velocity

		playerBody.AddForce (Vector3.up * bounceForce, ForceMode.Impulse);
		yield return new WaitForSeconds (.2f);
		delay = false;
	}

	void Update(){
		if (doIT) {
			doIT = false;
			playerBody1.AddForce (Vector3.up * bounceForce, ForceMode.Impulse);
		}
	}
}
