using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {
	public GameObject Ball;
	public int GAME_FRAME;
	public float d_from_ball;			//ボールとカメラの距離
	public float not_overlap_pos;		//カメラが壁と重ならない距離
	private int bump_flag = 0;				//ボールが壁と衝突したときのフラグ
	private int camera_pos_y = 1;		//カメラのy座標の底上げ
	private Vector3 bump_ball_v;			//ボールが衝突したときのボールの速度
	private Vector3 bump_ball_pos;		//ボールが衝突したときのボールの位置
	private Vector3 bump_cam_pos;		//ボールが衝突したときのカメラの位置
	private Vector3 fromPos;				//壁に衝突しない挙動を行うカメラの始点
	private Vector3 toPos;				//壁に衝突しない挙動を行うカメラの終点
	

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = GAME_FRAME;

		transform.position = new Vector3(0.0f, 0.0f, 0.0f);

/*		transform.position = new Vector3(
			Ball.transform.position.x,
			Ball.transform.position.y,
			Ball.transform.position.z
		);
*/	}
	
	// Update is called once per frame
	void Update () {
		//追跡するか，曲線運動するかの分岐　flag=0:通常追跡　flag=1:壁避け追跡
		if(bump_flag == 0){
			rigidbody.velocity = Ball.rigidbody.velocity;
			updatePosition();
		}
		else{
			avoidWall();
		}	
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

	//カメラの位置を更新
	void updatePosition () {
		//ボールの速度ベクトルの単位ベクトルを求める
		Vector3 e;	//ボールの速度の単位ベクトル
		e = calcUnitVector(Ball.rigidbody.velocity);

		//ボールの進む方向と逆方向の位置にカメラを置く
		Vector3 cam_pos;
		cam_pos = new Vector3(
			-d_from_ball*e.x + Ball.transform.position.x,
			-d_from_ball*e.y + Ball.transform.position.y + camera_pos_y,
			-d_from_ball*e.z + Ball.transform.position.z
		);
		transform.position = cam_pos;

		//カメラをボールの方向に向ける
		transform.LookAt(Ball.transform);
	}

	/*ボールが壁と衝突したときの処理
	Ball.csで呼び出すのでpublic*/
	public void changeBumpFlag(int x){
		//ボールが壁と衝突したときのボールの速度を記憶
		bump_ball_v = new Vector3(
			Ball.rigidbody.velocity.x,
			Ball.rigidbody.velocity.y,
			Ball.rigidbody.velocity.z
		);

		//ボールが壁と衝突したときのボールの位置を記憶
		bump_ball_pos = new Vector3(
			Ball.transform.position.x,
			Ball.transform.position.y + camera_pos_y,
			Ball.transform.position.z
		);

		//ボールが壁と衝突したときのカメラの位置を記憶
		bump_cam_pos = new Vector3(
			this.transform.position.x,
			this.transform.position.y,
			this.transform.position.z
		);

		//カメラが移動する始点と終点を決める
		fromPos = decideFromPos();
		toPos = decideToPos();

		//bump_flagを立てる
		bump_flag = x;
	}

	//カメラが壁を避けて動くための処理
	void avoidWall(){
		//カメラをfromPosからtoPosに移動させる
		//フレームで割ってボールに追いつかないようにする
/*		rigidbody.velocity = new Vector3(
			(toPos.x - fromPos.x) / GAME_FRAME/10,
			(toPos.y - fromPos.y) / GAME_FRAME/10,
			(toPos.z - fromPos.z) / GAME_FRAME/10
		);
*/		
		
		float x = 2.9f;

		rigidbody.velocity = new Vector3(
			x*(toPos.x - fromPos.x),
			x*(toPos.y - fromPos.y),
			x*(toPos.z - fromPos.z)
		);

		//カメラをボールの方向に向ける
		transform.LookAt(Ball.transform);

		//移動し終わったらbump_flagを0にし，通常のボール追従に戻る
		if(Vector3.Distance(transform.position, toPos) < 0.1){
			bump_flag = 0;
		}
	}

	//ボールが衝突したときのカメラ挙動，始点を定める
	Vector3 decideFromPos(){
		//始点
		return (bump_cam_pos);
	}

	//ボールが衝突したときのカメラ挙動，終点を定める
	Vector3 decideToPos(){
		//ボールが壁に衝突したときの速度の単位ベクトルを求める
		Vector3 e_bump_ball_v;	//ボールが壁に衝突したときの速度の単位ベクトル
		e_bump_ball_v = calcUnitVector(bump_ball_v);

		/*終点は，次に移動するボールの軌道上に置くが，
		その点は，始点から常に距離一定にしなければならない．
		そこで，ボールの速度ベクトルに定数倍かけていき，
		距離一定になる所で，終点を決定し，ループから抜ける．
		※あまりうまいやり方ではないのであとで変えるかも(´・ω・｀)
		*/
		Vector3 tmp_toPos;
		float standard = 1.0f;		//分解基準点
		float resolution = 0.1f;	//分解能

		tmp_toPos = bump_ball_pos;
		while(Vector3.Distance(fromPos, tmp_toPos) <= not_overlap_pos){
			//速度の単位ベクトルを小刻みに増やしてtoPosを求める
			tmp_toPos = new Vector3(
				standard*e_bump_ball_v.x + bump_ball_pos.x,
				standard*e_bump_ball_v.y + bump_ball_pos.y,
				standard*e_bump_ball_v.z + bump_ball_pos.z
			);
			standard += resolution;
		}
		
		return (tmp_toPos);
	}	
}
