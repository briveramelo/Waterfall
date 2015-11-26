using UnityEngine;
using System.Collections;

public class CubeGenerator : MonoBehaviour {

	public GameObject cube;
	public int numberPerLine;
	public float standardSpace;
	public float pauseTime;

	// Use this for initialization
	void Awake () {
		numberPerLine = 20;
		pauseTime = 0.001f;
		standardSpace = 1.25f;
	
		StartCoroutine (MakeACubeOfCubes ());
	}

	public IEnumerator MakeACubeOfCubes(){
		for (int x=0; x<numberPerLine; x++){
			for (int y=0; y<numberPerLine; y++){
				for (int z=0; z<numberPerLine; z++) {
					Instantiate(cube, new Vector3 (x,y,z) * standardSpace,Quaternion.identity);
				}
			}
			yield return new WaitForSeconds (pauseTime);
			
		}
		yield return null;
	}
}
