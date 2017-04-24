using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	Animator myAnimator;
	Rigidbody myRigidbody;
	//前進するための力
	float forwardForce = 800.0f;
	//左右に移動するための力
	float turnForce = 500.0f;
	//ジャンプするための力
	float upForce = 500.0f;
	//左右の移動できる範囲
	float movableRange = 3.4f;
	//動きを減速させる係数
	float coefficient = 0.95f;
	//ゲーム終了の判定
	bool isEnd = false;
	//ゲーム終了時に表示するテキスト
	GameObject stateText;
	//スコアを表示するテキスト
	GameObject scoreText;
	//得点
	int score = 0;

	//左ボタン押下の判定
	bool isLButtonDown = false;
	//右ボタン押下の判定
	bool isRButtonDown = false;

	void Awake() {
		//走るアニメーションを開始
		this.myAnimator = GetComponent<Animator> ();
		this.myAnimator.SetFloat ("Speed", 1);
		this.myRigidbody = GetComponent<Rigidbody> ();
		//シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");
		this.scoreText = GameObject.Find ("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {
		//ゲーム終了ならUnityちゃんの動きを減衰する(追加)
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}
		//前方へ力を加える
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);
	
		//Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			//左に移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
			//右に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		} 

		//JUMPしたら元に戻す
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		//ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			//選択した時に出る緑の矢印の方向に力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//トリガーモードの物体に衝突した時の処理
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//stateTextにGAME OVERを表示（追加）
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}
		//ゴール地点に到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			this.stateText.GetComponent<Text> ().text = "CLEAR!!";
		}
		if (other.gameObject.tag == "CoinTag") {
			this.score += 10;
			//スコアを常に表示する
			this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

			//パーティクルを再生
			GetComponent<ParticleSystem>().Play();
			//コインを削除
			Destroy (other.gameObject);
		}
	}

	//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown() {
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown() {
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理
	public void GetMyLeftButtonUp() {
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown() {
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理
	public void GetMyRightButtonUp() {
		this.isRButtonDown = false;
	}
}
