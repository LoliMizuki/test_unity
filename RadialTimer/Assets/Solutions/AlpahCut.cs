using UnityEngine;
using System.Collections;

public class AlpahCut : MonoBehaviour {

    public Mesh suckUnityUseThisWay;

    float _timeCount;
    GameObject _radialObj;

    void Awake() {
        _radialObj = new GameObject();

        MeshFilter mf = _radialObj.AddComponent<MeshFilter>();
        mf.mesh = suckUnityUseThisWay;

        MeshRenderer mr = _radialObj.AddComponent<MeshRenderer>();
		mr.material.mainTexture = Resources.Load<Texture2D>("round") as Texture2D;


		if(Shader.Find("Transparent/Cutout/VertexLit") == null) {
			Debug.Log("can not found this shader");
		}

//		_radialObj.transform.localScale = new Vector3(300, 300, 0);
		_radialObj.transform.position = new Vector3(0, 0, -50);
		_radialObj.renderer.material.shader = Shader.Find("Transparent/Cutout/VertexLit");
//		_radialObj.renderer.material.color = new Color(1, 1, 1, 0.3f);

        _timeCount = 0;

//		_radialObj.renderer.material.SetFloat("_Cutoff", 0.5f);
    }

    void Start() {
    
    }

    void Update() {
        float totalTime = 10;

        float tb = _timeCount / totalTime;
        if(tb > 1) {
            _timeCount = 0;
            tb = 1;
        }
	
        _radialObj.renderer.material.SetFloat("_Cutoff", tb/2.0f);

		_timeCount += Time.deltaTime;
    }
}
