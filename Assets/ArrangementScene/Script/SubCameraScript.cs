using UnityEngine;
using System.Collections;

public class SubCameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeSubCameraPosition(){
		transform.position = new Vector3(
			SelectedWallScript.selectedWall.transform.position.x - 10,
			SelectedWallScript.selectedWall.transform.position.y,
			SelectedWallScript.selectedWall.transform.position.z
		);
		transform.LookAt(SelectedWallScript.selectedWall.transform);
	}
}
