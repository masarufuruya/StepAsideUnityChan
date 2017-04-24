using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderController : MonoBehaviour {
	//アタッチしているオブジェクトが画面外に出たら自動削除
	//Rendererがついていないと実行されないのでコインにはMeshRenderを追加した
	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}
}
