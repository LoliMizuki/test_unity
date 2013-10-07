using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	exSpriteFont _exSpriteFont;

	void Awake()
	{
		_exSpriteFont = GetComponent<exSpriteFont>();
	}

	void Update()
	{
		_exSpriteFont.text = GameGlobal.GetInstance().GamePlayTime.ToString( "0.0" );
	}
}
