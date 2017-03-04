using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	public Texture startButton_tex;
	public Texture editEndButton_tex;
	public Texture settingButton_tex;
	public GUISkin button_skin;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//StartButton
		Rect startButton_pos = new Rect(0, 0, Screen.width/5, Screen.width/8);
		if(GUI.Button(startButton_pos, startButton_tex, button_skin.button)){
			Application.LoadLevel("GameScene");
		}

		//EditEndButton
		Rect editEndButton_pos = new Rect(0, Screen.height-Screen.width/8, Screen.width/5, Screen.width/8);
		if(VirtualWallScript.gameMode == 1){
			if(GUI.Button(editEndButton_pos, editEndButton_tex, button_skin.button)){
				VirtualWallScript.gameMode = 0;
			}
		}

		//SettingButton
		Rect settingButton_pos = new Rect(Screen.width/4*3+Screen.width/20, 0, Screen.width/5, Screen.width/8);
		if(GUI.Button(settingButton_pos, settingButton_tex, button_skin.button)){
			MainCameraPositioningScript.setting_f ++;
		}
	}
}
