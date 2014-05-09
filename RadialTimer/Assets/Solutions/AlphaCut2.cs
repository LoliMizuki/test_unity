using UnityEngine;
using System.Collections;

public class AlphaCut2 : MonoBehaviour {

	public Mesh suckUnityUseThisWay;

	float _timeCount;

	GameObject _under;
	GameObject _cdMask;

	void Awake() {
		_timeCount = 0;

		_under = new GameObject("i am button");
		_under.AddComponent<SpriteRenderer>();
		_under.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snm0006a");

		_cdMask = new GameObject("i am mask");
		_cdMask.AddComponent<MeshFilter>();
		_cdMask.GetComponent<MeshFilter>().mesh = suckUnityUseThisWay;
		_cdMask.AddComponent<MeshRenderer>();
		_cdMask.GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Cutout/VertexLit");
		_cdMask.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture2D>("block") as Texture2D;

		_cdMask.transform.position = new Vector3(0,0,-4);
	}

	void Update() {
		float totalTime = 10;
		float tb = _timeCount / totalTime;
		if(tb > 1) {
			tb = 1;
			_timeCount = 0;
		}

		_cdMask.renderer.material.SetFloat("_Cutoff", tb/2);

		_timeCount += Time.deltaTime;
	}
}
