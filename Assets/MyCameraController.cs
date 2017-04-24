using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {
	GameObject unitychan;
	float differece;

	void Awake() {
		this.unitychan = GameObject.Find ("unitychan");
		//Unityちゃんとカメラの位置の差を求める
		this.differece = unitychan.transform.position.z - this.transform.position.z;
	}
			
	// Update is called once per frame
	void Update () {
		//最初の位置をキープする
		this.transform.position = new Vector3 (0, this.transform.position.y, this.unitychan.transform.position.z - differece);
	}
}
