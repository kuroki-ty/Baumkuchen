using UnityEngine;
using System.Collections;

public class TryAgainButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		Rect Button_pos = new Rect(0, Screen.height/2-Screen.height/10, Screen.width/3, Screen.height/10);
		if(GUI.Button(Button_pos, "Try Again")){
			Application.LoadLevel("ArrangementScene");
		}
	}
}
