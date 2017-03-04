using UnityEngine;
using System.Collections;

public class MainCameraPositioningScript : MonoBehaviour {
	public GameObject mostLeftObj;		//画面左端のVirtualWall
	public GameObject mostRightObj;		//画面右端のvirtualWall
	static public int setting_f = 0;	//カメラ操作するボタン群の表示/非常時切り替え用フラグ

	private Vector3 pre_camera_pos;	//カメラの初期位置
	private float val = 60.0f;		//カメラのFieldOfView 初期値60
//	private Vector3 toRightPos;		//カメラ回転移動時の右終点
//	private Vector3 toLeftPos;		//カメラ回転移動時の左終点


	// Use this for initialization
	void Start () {
		//カメラ初期位置を計算(全オブジェクトが写るように)
		pre_camera_pos = new Vector3(
			(mostLeftObj.transform.position.x + mostRightObj.transform.position.x) / 2.0f,
			(mostLeftObj.transform.position.y + mostRightObj.transform.position.y) / 2.0f,
			(mostLeftObj.transform.position.z + mostRightObj.transform.position.z) / 2.0f
		);

/*		//カメラ回転移動時の右終点決定
		toRightPos =  new Vector3(
			(mostLeftObj.transform.position.x + mostRightObj.transform.position.x) / 2.0f,
			0.0f,
			(mostLeftObj.transform.position.z + mostRightObj.transform.position.z) / 2.0f - 10.0f
		);

		//カメラ回転移動時の左終点決定
		toLeftPos =  new Vector3(
			(mostLeftObj.transform.position.x + mostRightObj.transform.position.x) / 2.0f,
			0.0f,
			(mostLeftObj.transform.position.z + mostRightObj.transform.position.z) / 2.0f + 10.0f
		);
*/
		//カメラ初期位置
		transform.position = new Vector3(pre_camera_pos.x, 50.0f, pre_camera_pos.z);
		transform.rotation = Quaternion.Euler(90, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//設定ボタンをクリックした場合のみ表示させる
		if(setting_f%2 == 1){
			GUI.Box(new Rect(Screen.width/4*1, 0, Screen.width/4*2, Screen.height/5), "カメラ操作");

			//カメラ視野の変更
			val = GUI.HorizontalScrollbar(new Rect(Screen.width/4*1, Screen.height/15*1, Screen.width/4*2, Screen.height/15), val, 5, 30, 80);
			camera.fieldOfView = val;

			//視点変更
			if(GUI.Button(new Rect(Screen.width/4*1+Screen.width/8*1, Screen.height/15*2, Screen.width/4*1, Screen.height/15), "視点変更")){
				//縦視点→横視点
				if(transform.rotation == Quaternion.Euler(90, 90, 0)){
					transform.position = new Vector3(-50.0f, pre_camera_pos.y, pre_camera_pos.z);
					transform.rotation = Quaternion.Euler(0, 90, 0);

				}
				//横視点→縦視点
				else if(transform.rotation == Quaternion.Euler(0, 90, 0)){
					transform.position = new Vector3(pre_camera_pos.x, 50.0f, pre_camera_pos.z);
					transform.rotation = Quaternion.Euler(90, 90, 0);
				}
			}
		}
		
	}
}