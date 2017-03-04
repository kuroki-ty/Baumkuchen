using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public GameObject Camera;
	public float constant_v;

	// Use this for initialization
	void Awake () {
		transform.rigidbody.AddForce(new Vector3(1000, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
	}

	//物体と衝突した瞬間に実行される
	void OnCollisionEnter(Collision collision){
		/*ボールの方向にRayを距離100で飛ばし，
		ボールが壁と衝突する可能性があるかどうかを判断する
		trueなら，その壁の情報を引っ張ってくる*/
		RaycastHit hit_wall;
		if(Physics.Raycast(transform.position, rigidbody.velocity, out hit_wall, 100)){
			updateVelocity(hit_wall);
		}
		else{
			StartCoroutine("GameOver", 2.0f);
			
		}

		//衝突したときにMainCamera.csのflag関数を呼び出す
		MainCameraScript mainCamera = Camera.GetComponent<MainCameraScript>();
		mainCamera.changeBumpFlag(1);
	}

	//IsTriggerがオンになっている物体と衝突したときに実行される
	void OnTriggerEnter(Collider other){
		Debug.Log("Goal!");
	}

	/*単位ベクトルを求める
	引数:単位ベクトルにしたいベクトル
	戻値:単位ベクトル
	*/
	Vector3 calcUnitVector(Vector3 u){
		float nolum;	//ベクトルの大きさ
		Vector3 e;		//単位ベクトル
	
		nolum = Mathf.Sqrt(u.x*u.x + u.y*u.y + u.z*u.z);
		e = new Vector3(u.x/nolum, u.y/nolum, u.z/nolum);

		return (e);
	}

	//ボールを壁の中心に向かわせるような速度ベクトルを計算する
	void updateVelocity(RaycastHit wall){
		
		//壁の中心位置とボールの現在位置からそのベクトルを計算する
		Vector3 v;	//ボールから壁までのベクトル
		v = new Vector3(
			wall.transform.position.x - this.transform.position.x,
			wall.transform.position.y - this.transform.position.y,
			wall.transform.position.z - this.transform.position.z
			);

		//vの単位ベクトルを求める
		Vector3 e;		//単位ベクトル
		e = calcUnitVector(v);

		//単位ベクトルに速度倍率をかける(一定速度に保つ)
		Vector3 ball_v;
		ball_v = new Vector3(
			constant_v * e.x,
			constant_v * e.y,
			constant_v * e.z
			);

		//ボールの速度を更新する
		this.rigidbody.velocity = ball_v;
	}

	IEnumerator GameOver(){
		yield return new WaitForSeconds(2);	//2秒待機
		Destroy(GameObject.Find("Walls"));
		Application.LoadLevel("GameOverScene");
	}
}