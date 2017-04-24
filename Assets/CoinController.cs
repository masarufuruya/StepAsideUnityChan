using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	void Awake() {
		//初期の回転角度を設定
		//コイン毎に回転がズレるように乱数を使う
		this.transform.Rotate (0, Random.Range (0, 360), 0);
	}

	// Update is called once per frame
	void Update () {
		//回転
		this.transform.Rotate (0, 3, 0);
	}
}
