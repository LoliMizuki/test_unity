using UnityEngine;
using System.Collections;

public class DangoInfo
{
	public enum DangoType
	{
		Unkonw = 0,
		Type1,
		Type2,
		Type3,
		Type4,
		Type5,
		Type6,
		Type7,
	}

	static public Color DangoColorByType(DangoType dangoType)
	{
		switch( dangoType )
		{
			case DangoType.Type1:
				return Color.red;

			case DangoType.Type2:
				return Color.green;

			case DangoType.Type3:
				return Color.blue;

			case DangoType.Type4:
				return Color.yellow;

			case DangoType.Type5:
				return new Color( 0.819f, 0.988f, 0.560f );

			case DangoType.Type6:
				return new Color( 0.972f, 0.0f, 0.854f );
		}

		return Color.black;
	}

	static public DangoType GetRandomType(int randRange)
	{
		int numDangoTypes = System.Enum.GetNames( typeof( DangoInfo.DangoType ) ).Length - 1;
		if( randRange > numDangoTypes )
			randRange = numDangoTypes;

		return (DangoInfo.DangoType)MZMath.RandomFromRange( 1, randRange );
	}
}

public class DangoControl : MonoBehaviour
{
	public bool IsActive = false;
	public AudioClip SE_HitBody;
	public AudioClip SE_HitKuji;

	enum UpdateState
	{
		Normal,
		Fall,
	}

	#region - move
	float _currVelocity = 0;
	float _currDirection = 0;
	Vector2 _currAcc = Vector2.zero;
	float _moveTimeCount;
	#endregion
	UpdateState _currUpdateState;
	float _lifeTimeCount;
	DangoInfo.DangoType _currDangoType = DangoInfo.DangoType.Unkonw;

	public DangoInfo.DangoType CurrDangoType
	{
		set
		{
			_currDangoType = value;
			SetColorWithDangoType( _currDangoType );
		}
		get{ return _currDangoType; }
	}

	public void Spawn(Vector3 position)
	{
		IsActive = true;

		this.gameObject.SetActive( true );
		this.gameObject.transform.position = position;
		CurrDangoType = DangoInfo.GetRandomType( GameGlobal.GetInstance().GetDangonTypesNumberCeiling );

		_lifeTimeCount = 0.0f;
		_moveTimeCount = 0.0f;
		_currDirection = MZMath.DegreesFromP1ToP2( position, Vector2.zero );
		_currVelocity = 100 + (float)MZMath.RandomFromRange( 0, 50 );
		_currAcc = Vector2.zero;
		_currUpdateState = UpdateState.Normal;
	}

	public void Disable()
	{
		IsActive = false;
		this.gameObject.SetActive( false );
	}

	void Awake()
	{

	}

	void Start()
	{
		
	}

	void Update()
	{
		_lifeTimeCount += Time.deltaTime;
		UpdateMove();
		CheckAndDisableCondistions();
	}

	void UpdateMove()
	{
		_moveTimeCount += Time.deltaTime;

		Vector2 mv = MZMath.UnitVectorFromDegrees( _currDirection );
		float deltaMove = _currVelocity*Time.deltaTime;

		Vector2 deltaMoveXY = deltaMove*mv;
		deltaMoveXY += _currAcc*_moveTimeCount;

		gameObject.transform.localPosition = new Vector3(
			gameObject.transform.position.x + deltaMoveXY.x,
			gameObject.transform.position.y + deltaMoveXY.y,
			0 );
	}

	void CheckAndDisableCondistions()
	{
		Vector2 currPos = this.gameObject.transform.position;
		if( GameGlobal.GetInstance().DangoSpawnRange.Contains( currPos ) == false )
		{
			IsActive = false;
			this.gameObject.SetActive( false );
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if( _currUpdateState == UpdateState.Fall )
			return;

		CheckKujiTrigger( other );
		CheckYuyukoBodyTrigger( other );
	}

	void CheckKujiTrigger(Collider other)
	{
		if( other.gameObject.name != "Kuji" )
			return;

		GameGlobal.GetInstance().PlaySE( SE_HitKuji );

		other.gameObject.SendMessage( "AddDangoWithType", CurrDangoType, SendMessageOptions.DontRequireReceiver );

		Disable();
	}

	void CheckYuyukoBodyTrigger(Collider other)
	{
		if( other.gameObject.name != "YuyukoBody" )
			return;

		GameGlobal.GetInstance().PlaySE( SE_HitBody );

		other.gameObject.SendMessage( "DoHitten", this.gameObject.transform.position, SendMessageOptions.DontRequireReceiver );
		_currUpdateState = UpdateState.Fall;
		GetComponent<exSprite>().color = new Color(
			GetComponent<exSprite>().color.r,
			GetComponent<exSprite>().color.g,
			GetComponent<exSprite>().color.b,
			0.5f );

		_currDirection = 180 + _currDirection;
		_currAcc = new Vector2( 0, -10 );
		_moveTimeCount = 0.0f;
	}

	void SetColorWithDangoType(DangoInfo.DangoType dangoType)
	{
		exSprite s = this.gameObject.GetComponent<exSprite>();
		s.color = DangoInfo.DangoColorByType( _currDangoType );
	}
}
