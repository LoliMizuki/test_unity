using UnityEngine;
using System.Collections;

public class Pixel : MonoBehaviour {

	GameObject _obj;
	Texture2D _texture;

	float _timeCount;

	void Awake() {
		_obj = new GameObject();

		SpriteRenderer sr = _obj.AddComponent<SpriteRenderer>();
		_texture = (Texture2D)Texture2D.Instantiate(Resources.Load<Texture2D>("adyuyuko"));
		sr.sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100);
		sr.renderer.material.color = new Color(1, 1, 1, 0.5f);
		_obj.transform.localScale = new Vector3(0.5f, 0.5f, 0);

		GameObject g = new GameObject("I use the same texture !!, so I suck as he(unity sucks :D)");
		SpriteRenderer sr2 = g.AddComponent<SpriteRenderer>();
		Texture2D t2 = Resources.Load<Texture2D>("adyuyuko");
		sr2.sprite = Sprite.Create(t2, new Rect(0, 0, t2.width, t2.height), new Vector2(0.5f, 0.5f), 100);
	
		GameObject bg = new GameObject();
		bg.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("yuyuko");
		bg.transform.localScale = new Vector3(0.005f, 0.005f, 0);
		bg.transform.position = new Vector3(0, 0, 10);

		_timeCount = 0;
	}
	
	void Start() {
	}

	void Update() {
		float totalTime = 10;

		float tb = _timeCount / totalTime;
		if(tb > 1) {
			tb = 1;
			_timeCount = 0;
		} 

		float currRad = tb * (Mathf.PI * 2);
		Vector2 center = new Vector2(_texture.width / 2, _texture.height / 2);

		for(int x = 0; x < _texture.width; x++) {
			for(int y = 0; y < _texture.height; y++) {

				float dx = x - center.x;
				float dy = y - center.y;

				float theta = Mathf.Atan2(dy ,dx);
				if(theta < 0) theta = (Mathf.PI * 2) + theta;

				Color originColor = _texture.GetPixel(x, y);

				if(theta < currRad) {
					_texture.SetPixel(x, y, new Color(originColor.r, originColor.g, originColor.b, 0.5f));
				} else {
					_texture.SetPixel(x, y, new Color(originColor.r, originColor.g, originColor.b, 0));
				}
			}
		}

		_texture.Apply();

		_timeCount += Time.deltaTime;
	}
}
