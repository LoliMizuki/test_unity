using UnityEngine;
using System.Collections.Generic;


public class AnimationSol : MonoBehaviour {

	List<Sprite> _frames;
	int _frameCount;

	float _interval;
	float _timeCount;

	GameObject _obj;
	GameObject _mask;

	void Awake() {
		_frames = new List<Sprite>();

		int maxFrame = 36;

		for(int i = 0; i <= 36; i++) {
			string name = string.Format("cd_timer_mask_{0:0000}", i);
			_frames.Add(Resources.Load<Sprite>("frames2/" + name));
		}

		_obj = new GameObject("I want to CD");
		_obj.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snm0006a");

		_mask = new GameObject("I give you mask");
		_mask.AddComponent<SpriteRenderer>().sprite = _frames[0];
		_mask.transform.position = new Vector3(0, 0, -5);
		_mask.transform.localScale = new Vector3(2, 2, 0);
		_mask.renderer.material.color = new Color(1, 1, 1, 0.5f);

		_frameCount = 0;
		_interval = 0.05f;
		_timeCount = _interval;
	}

	void Update() {
		_timeCount -= Time.deltaTime;

		if(_timeCount > 0) {
			return;
		}

		_frameCount++;
		_timeCount += _interval;

		_mask.GetComponent<SpriteRenderer>().sprite = _frames[_frameCount % _frames.Count];
	}
}
