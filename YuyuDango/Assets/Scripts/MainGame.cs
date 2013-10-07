using UnityEngine;
using System;
using System.Collections.Generic;

public class GameGlobal
{
	static GameGlobal _instance = null;
	public Rect DangoSpawnRange;
	public Rect PlayerMovableBoundary;

	#region - GUI
	public GameObject PlayerObject;
	public GameObject GUI_TargetDangos;
	public TimerGaugeControl TimerGaugeControl;
	public MatchLabelControl MatchLabelControl;
	public exSpriteFont ScoreFont;
	public exSpriteFont ComboFont;
	#endregion

	#region - AudioPlayer
	public AudioSource AudioPlayer;
	#endregion

	#region - Sound Clip
	public AudioClip SE_MatchDangos;
	#endregion

	#region - GamePlay
	float _timeLifeMax = 60;
	float _gamePlayTime;
	DangoInfo.DangoType[] _currTargetDangoTypesPattern = new DangoInfo.DangoType[3];
	float __currTimeLifeCount;

	float _currTimeLifeCount
	{
		set
		{
			__currTimeLifeCount = value;
			if( __currTimeLifeCount > _timeLifeMax )
				__currTimeLifeCount = _timeLifeMax;
			if( __currTimeLifeCount < 0 )
				__currTimeLifeCount = 0;
		}

		get { return __currTimeLifeCount; }
	}
	#endregion

	#region - Score
	int _score = 0;
	int _combo = 0;
	#endregion

	public float CurrTimeLifeCount
	{ get { return _currTimeLifeCount; } }

	public float TimeLifeMax
	{
		get{ return _timeLifeMax; }
	}

	public DangoInfo.DangoType[] CurrTargetDangoTypesPattern
	{ get { return _currTargetDangoTypesPattern; } }

	public float GamePlayTime
	{ get { return _gamePlayTime; } }

	static public GameGlobal GetInstance()
	{
		if( _instance == null )
			_instance = new GameGlobal();
		return _instance;
	}

	#region - GamePlay
	public void ResetGame()
	{
		_gamePlayTime = 0.0f;
		Application.LoadLevel( "TitleScene" );
	}

	public int GetDangonTypesNumberCeiling
	{
		get
		{
			return ((int)_gamePlayTime/30) + 2;
		}
	}

	public void CreateNewTargetDangoPattern()
	{
		int dangonTypesNumberCeiling = this.GetDangonTypesNumberCeiling;
		for( int i = 0; i < _currTargetDangoTypesPattern.Length; i++ )
		{
			_currTargetDangoTypesPattern[ i ] = DangoInfo.GetRandomType( dangonTypesNumberCeiling );
		}

		if( GUI_TargetDangos == null )
		{
			return;
		}

		GUI_TargetDangos.SendMessage( "SetTargetDangoPattern", _currTargetDangoTypesPattern, SendMessageOptions.RequireReceiver );
	}

	public void DoDangosMatchAction(int matchCount)
	{
		PlaySE( SE_MatchDangos );

		_currTimeLifeCount += 10*( matchCount );
		GUI_TargetDangos.transform.FindChild( "Fan" ).gameObject.GetComponent<FanAction>().StartAction();

		Func<int,string> GetMatchText = (int count) =>
		{
			switch( matchCount )
			{
				case 0:
					return "Bad";
				case 1:
					return "Good!";
				case 2:
					return "Great!!";
				case 3:
					return "Perfect!!!";
			}

			return "";
		};

		MatchLabelControl.StartWithMessage( GetMatchText( matchCount ) );

		_score = ( matchCount > 0 )? _score + matchCount*100*(int)( _combo*1.1f ) : _score;
		_combo = ( matchCount > 0 )? _combo + matchCount : 0;

		ScoreFont.text = _score.ToString();
		ComboFont.text = _combo.ToString() + "Combo";
	}

	public void DoDangoHitYuyukoBody()
	{
		_currTimeLifeCount -= 5;
	}

	public void Update()
	{
		_gamePlayTime += Time.deltaTime;
		__currTimeLifeCount -= Time.deltaTime;
	}
	#endregion

	public void PlaySE(AudioClip audioClip)
	{
		AudioPlayer.clip = audioClip;
		AudioPlayer.Play();
	}

	public void DrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(
			new Vector3( DangoSpawnRange.center.x, DangoSpawnRange.center.y, 0 ),
			new Vector3( DangoSpawnRange.width, DangoSpawnRange.height, 1 ) );

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(
			new Vector3( PlayerMovableBoundary.center.x, PlayerMovableBoundary.center.y, 0 ),
			new Vector3( PlayerMovableBoundary.width, PlayerMovableBoundary.height, 1 ) );
	}

	private GameGlobal()
	{
		Vector2 screenSize = new Vector2( 768, 1024 );

		Vector2 dangoSpawnRangeSize = screenSize*1.2f;
		DangoSpawnRange = new Rect( -dangoSpawnRangeSize.x/2, -dangoSpawnRangeSize.y/2, dangoSpawnRangeSize.x, dangoSpawnRangeSize.y );

		Vector2 playerMovableBoundarySize = screenSize*0.8f;
		Vector2 playerMovableCenterOffset = new Vector2( 0, -80 );
		float scaleY = 0.8f;
		PlayerMovableBoundary = new Rect(
		playerMovableCenterOffset.x - playerMovableBoundarySize.x/2,
			playerMovableCenterOffset.y - playerMovableBoundarySize.y*scaleY/2,
			playerMovableBoundarySize.x,
			playerMovableBoundarySize.y*scaleY );

		__currTimeLifeCount = _timeLifeMax;
	}
}

public class MainGame : MonoBehaviour
{
	#region Play Objects
	public GameObject PlayerObject;
	#endregion

	#region GUI
	public GameObject GUI_TargetDangos;
	public GameObject TimerGauge;
	public GameObject MatchLabel;
	public GameObject ScoreLabel;
	public GameObject ComboLabel;
	#endregion

	#region SE
	public GameObject SEPlayerObject;
	public AudioClip SE_MatchDangos;
	#endregion

	void Awake()
	{
		GameGlobal.GetInstance().PlayerObject = PlayerObject;
		GameGlobal.GetInstance().GUI_TargetDangos = GUI_TargetDangos;
		GameGlobal.GetInstance().TimerGaugeControl = TimerGauge.GetComponent<TimerGaugeControl>();

		GameGlobal.GetInstance().CreateNewTargetDangoPattern();

		GameObject fanObject = GUI_TargetDangos.transform.FindChild( "Fan" ).gameObject;
		MZDebug.Assert( fanObject != null, "not found fan object" );
		FanAction fanAction = fanObject.GetComponent<FanAction>();
		MZDebug.Assert( fanAction != null, "can not get fanAction in GUI_TargetDangos children" );
		fanAction.actionDegrees = 180;
		fanAction.OnDegreesEvent += () => {
			GameGlobal.GetInstance().CreateNewTargetDangoPattern(); };

		GameGlobal.GetInstance().MatchLabelControl = MatchLabel.GetComponent<MatchLabelControl>();
		GameGlobal.GetInstance().AudioPlayer = SEPlayerObject.GetComponent<AudioSource>();

		GameGlobal.GetInstance().ScoreFont = ScoreLabel.GetComponent<exSpriteFont>();
		GameGlobal.GetInstance().ComboFont = ComboLabel.GetComponent<exSpriteFont>();

		GameGlobal.GetInstance().SE_MatchDangos = SE_MatchDangos;
	}

	void Update()
	{
		GameGlobal.GetInstance().Update();
	}

	void OnDrawGizmos()
	{
		GameGlobal.GetInstance().DrawGizmos();
	}
}
