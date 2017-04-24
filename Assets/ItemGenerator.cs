using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	//インスペクタで生成するプレハブを指定
	public GameObject carPrefab;
	public GameObject coinPrefab;
	public GameObject conePrefab;

	int startPos = -160;
	int goalPos = 120;
	float posRange = 3.4f;

	//ゴールまでの間にアイテムを自動生成
	//Itemの種類を0-11の間の数値で判定してるけどマジックナンバーになっているので
	//Itemクラスにまとめて敷居値は定数、判定や生成処理は関数にまとめたい
	void Awake() {
		//zに15ずつスペースをあけてアイテムを生成
		for (int i = startPos; i < goalPos; i += 15) {
			//どのアイテムを出すのかはランダムに設定
			int num = Random.Range(0, 10);
			if (num <= 1) {
				//コーンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					//生成したオブジェクトになんらかの処理をいたい時はas GameObjectをつける
					//一度ゲームオブジェクトにする必要ない気もする...
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
				}
			} else {
				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range(-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin = Instantiate (coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car = Instantiate (carPrefab) as GameObject;
						car.transform.position = new Vector3 (posRange * j, car.transform.position.y, i + offsetZ);
					}
				}
			}
		}
	}

}
