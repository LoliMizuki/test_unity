using UnityEngine;
using System.Collections;

public class MatchLabelControl : MonoBehaviour
{
	bool _isActive;
	float _lifeTimeCount;
	float _targetScale;
	exViewportPosition _exViewportPosition;
	float _labelViewportPositionY;
	exSpriteFont _exSpriteFont;

	public void StartWithMessage(string message)
	{
		_exSpriteFont.text = message;

		_isActive = true;
		_lifeTimeCount = 0;
		_exSpriteFont.scale = Vector2.zero;
		gameObject.SetActive( true );

		_exViewportPosition.y = _labelViewportPositionY;
	}

	void Awake()
	{
		_exSpriteFont = GetComponent<exSpriteFont>();

		_isActive = false;
		_targetScale = _exSpriteFont.scale.x;
		_exSpriteFont.scale = Vector2.zero;

		_exViewportPosition = GetComponent<exViewportPosition>();
		_labelViewportPositionY = _exViewportPosition.y;
	}

	void Update()
	{
		if( _isActive == false )
			return;

		_lifeTimeCount += Time.deltaTime;

		float timeDurationShow = 0.6f;
		float timeToDisable = timeDurationShow + 1.6f;

		if( _lifeTimeCount <= timeDurationShow ) // rotation to show
		{
			float currScale = ( _lifeTimeCount/timeDurationShow )*_targetScale;
			_exSpriteFont.scale = new Vector2( currScale, currScale );

			float rotBase = _lifeTimeCount/timeDurationShow;
			if( rotBase > 1 )
				rotBase = 1;

			gameObject.transform.localRotation = Quaternion.Euler( 0, 0, rotBase*360.0f*5 );
		}
		else // fadeOut and fall(not work, bcz alpha!!!)
		{
			gameObject.transform.localRotation = Quaternion.Euler( 0, 0, 0 );
//			_exViewportPosition.y -= 0.01f*Time.deltaTime;
//
//			float interval = timeToDisable - timeDurationShow;
//			float fadeOutCount = _lifeTimeCount - timeDurationShow;
//			float alpha = 1.0f - fadeOutCount/interval;
//			Color topColor = _exSpriteFont.topColor;
//			Color botColor = _exSpriteFont.botColor;
//			Color outlineColor = _exSpriteFont.outlineColor;
//			_exSpriteFont.topColor = new Color( topColor.r, topColor.g, topColor.b, alpha );
//			_exSpriteFont.botColor = new Color( botColor.r, botColor.g, botColor.b, alpha );
//			_exSpriteFont.outlineColor = new Color( outlineColor.r, outlineColor.g, outlineColor.b, alpha );
		}

		// disable
		if( _lifeTimeCount >= timeToDisable )
		{
			_isActive = false;
			gameObject.SetActive( false );
		}
	}
}
