using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CheckPointManager : MonoBehaviour {

	public GameObject newPlayer;
	public Transform playerTransform;
	public Vector3[] checkpoints;
	public int currentCheckPoint;
	private string playerPath;
	private string objectiveCameraPath;

	void Awake(){
		playerPath = "StandardAssets/Characters/FirstPersonCharacter/Prefabs/RigidBodyFPSController";
		objectiveCameraPath = "Prefabs/ObjectiveCamera";
		checkpoints = new Vector3[transform.GetComponentsInChildren<Transform> ().Length];
		int i = 0;
		foreach (CheckPoint child in transform.GetComponentsInChildren<CheckPoint>()) {
			checkpoints[i] = child.transform.position + Vector3.up * 2f;
			child.checkPointNumber = i;
			child.checkPointManager = this;
			i++;
		}

		foreach (OutOfBounds boundScript in GameObject.FindObjectsOfType<OutOfBounds>()) {
			boundScript.checkPointManager = this;
		}
	}
	
	public IEnumerator ResetPosition(GameObject player){
		Vector3 camPosition = player.transform.GetChild(0).position;
		Quaternion camRotation = player.transform.GetChild(0).rotation;
		Destroy (player);

		GameObject newGuy = Instantiate (Resources.Load (playerPath),checkpoints[currentCheckPoint],Quaternion.identity) as GameObject;
		yield return null;
		GameObject objectiveCamera = Instantiate (Resources.Load (objectiveCameraPath), camPosition, camRotation) as GameObject;
		yield return null;
	}
}
