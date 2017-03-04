using UnityEngine;
using System.Collections;

public class WallsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);	//Wallsの子オブジェクトは次のシーンに引き継ぐ
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
