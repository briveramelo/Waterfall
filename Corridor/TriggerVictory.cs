using UnityEngine;
using System.Collections;

public class TriggerVictory : MonoBehaviour {

	private bool entered;
	public ParticleSystem fireworks;
	public AudioSource youWin;

	void OnTriggerEnter(Collider col){
		if (!entered && col.gameObject.layer == 8) {
			StartCoroutine (Fireworks());
		}
	}

	public IEnumerator Fireworks(){
		entered =true;
		fireworks.enableEmission = true;
		youWin.Play ();
		yield return null;
	}

}
