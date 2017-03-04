using UnityEngine;
using System.Collections;

public class ScreenPartitionScript : MonoBehaviour {
	public Camera mainCamera;
	public Camera subCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(VirtualWallScript.gameMode == 1){
			mainCamera.rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
			subCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
		}
		else{
			mainCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
		}
	}
}
