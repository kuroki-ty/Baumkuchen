using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	private float touchMousePositionX;
	private float touchMousePositionY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		//マウスをクリックしたときに呼び出される
	void OnMouseDown(){
		//マウスをクリックしたときのスクリーン座標を取得する
		touchMousePositionX = Input.mousePosition.x;
		touchMousePositionY = Input.mousePosition.y;
	}

	//マウスをドラッグしたときに呼び出される
	void OnMouseDrag(){
		/*変化量を取る*/
		float delta_x, delta_y;		//ドラッグしたときのマウスの変化量
		int direction = -1;			//方向　上:0 右:1 下:2 左:3 例外:-1

		delta_x = Input.mousePosition.x - touchMousePositionX;
		delta_y = Input.mousePosition.y - touchMousePositionY;

		//変化量が一番大きい方向を判断する
		//Mathf.Sign():符号．正ならば1，負ならば-1，0なら0
		if(Mathf.Abs(delta_x) > Mathf.Abs(delta_y)){
			if(Mathf.Sign(delta_x) == 1){
				direction = 1;
			}
			else if(Mathf.Sign(delta_x) == -1){
				direction = 3;
			}
		}
		else if(Mathf.Abs(delta_x) < Mathf.Abs(delta_y)){
			if(Mathf.Sign(delta_y) == 1){
				direction = 0;
			}
			else if(Mathf.Sign(delta_y) == -1){
				direction = 2;
			}
		}

		//オブジェクトを回転させる
		//Mathf.MoveTowardsAngle(現在の角度，目的角度，スピード):角度を指定したスピードで目的角度まで移動させる
		//注意1:上下回転の目的角度が88以上の場合，謎のエラーでオブジェクトが回転しなくなる
		//注意2:左右回転の目的角度を90にした場合，通常の回転とは逆回転で目的角度まで向かう
		switch(direction){
			case 0:			//上ドラッグ
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.MoveTowardsAngle(transform.eulerAngles.z, -87, 45*Time.deltaTime));
				break;
			case 1:			//右ドラッグ
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.MoveTowardsAngle(transform.eulerAngles.y, -89, 45*Time.deltaTime), transform.eulerAngles.z);
				break;
			case 2:			//下ドラッグ
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.MoveTowardsAngle(transform.eulerAngles.z, 87, 45*Time.deltaTime));
				break;
			case 3:			//左ドラッグ
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.MoveTowardsAngle(transform.eulerAngles.y, 89, 45*Time.deltaTime), transform.eulerAngles.z);
				break;
			default:		//例外処理
				break;
		}
	}
}
