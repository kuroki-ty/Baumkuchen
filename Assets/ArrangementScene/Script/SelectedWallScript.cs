using UnityEngine;
using System.Collections;

public class SelectedWallScript : MonoBehaviour {
	static public GameObject selectedWall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray;			//飛ばすRayの情報
			RaycastHit hit; 	//Rayから取得するゲームオブジェクト情報
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100)){
				selectedWall = hit.collider.gameObject;	//Rayに当たったオブジェクトの情報を格納
			}
			else{
				selectedWall = null;
			}	
		}
		
	}
}
