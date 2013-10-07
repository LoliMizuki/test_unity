using UnityEngine;
using System.Collections;

public class TimerGaugeControl : MonoBehaviour
{
	exSprite _exSprite;
	float _totalScaleX;
	// 0 ~ 100
	float _Gauge
	{
		set{ _exSprite.scale = new Vector2( ( value/100.0f )*_totalScaleX, _exSprite.scale.y ); }
		get{ return _exSprite.scale.x/_totalScaleX*100.0f; }
	}

	void Awake()
	{
		_exSprite = gameObject.GetComponent<exSprite>();
		_totalScaleX = _exSprite.scale.x;
	}

	void Start()
	{

	}

	void Update()
	{
		float newScaleXBase = GameGlobal.GetInstance().CurrTimeLifeCount/GameGlobal.GetInstance().TimeLifeMax;
		if( newScaleXBase < 0 )
			newScaleXBase = 0;
		if( newScaleXBase > 1 )
			newScaleXBase = 1;

		float newGuauge = 100.0f*newScaleXBase;
		if( newGuauge - _Gauge >= 5 )
			_Gauge += 0.5f;
		else
			_Gauge = newGuauge;
	}
}
