using UnityEngine;
using System.Collections;

public class BarrierMoveIntoPlace : MonoBehaviour {

	public Transform targetTransform;
	private float moveSpeed;

	void Awake(){
		moveSpeed = 10f;
		StartCoroutine (MoveIntoPlace ());
	}

	public IEnumerator MoveIntoPlace(){
		yield return new WaitForSeconds (4f);
		while (Vector3.Distance(transform.position, targetTransform.position) > 0.01f) {
			transform.position = Vector3.MoveTowards(transform.position,targetTransform.position, Time.deltaTime * moveSpeed);
			yield return null;
		}
		yield return null;
	}
}
