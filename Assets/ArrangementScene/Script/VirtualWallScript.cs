using UnityEngine;
using System.Collections;

public class VirtualWallScript : MonoBehaviour {
	static public int gameMode = 0;		//ゲームモード　通常モード:0，エディットモード:1
	public GameObject wallPrefab;
	private GameObject wallClone;
	private GameObject walls;
	private GameObject subCamera;


	// Use this for initialization
	void Start () {
		walls = GameObject.Find("Walls");
		subCamera = GameObject.Find("Sub Camera");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		if(SelectedWallScript.selectedWall != null && SelectedWallScript.selectedWall.tag == "VirtualWall"){
			wallClone = (GameObject)Instantiate(wallPrefab, SelectedWallScript.selectedWall.transform.position, Quaternion.identity);
			wallClone.transform.parent = walls.transform;

			SubCameraScript subCameraScript = subCamera.GetComponent<SubCameraScript>();
			subCameraScript.changeSubCameraPosition();

			Destroy(SelectedWallScript.selectedWall);
			SelectedWallScript.selectedWall = null;

			gameMode = 1;
		}
	}
}
