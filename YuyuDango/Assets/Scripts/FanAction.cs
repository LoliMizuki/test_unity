using UnityEngine;
using System.Collections;

public class FanAction : MonoBehaviour
{
	public float actionDegrees = 0;
	public delegate void DegreesAction();
	public event DegreesAction OnDegreesEvent;

	bool _doAction;
	bool _doneDegreesAction;
	float _startDegrees = 90;
	float _endDegrees = 270;

	float _degrees
	{
		set { gameObject.transform.rotation = Quaternion.Euler( 0, 0, value ); }
		get { return gameObject.transform.rotation.eulerAngles.z; }

	}

	public void StartAction()
	{
		gameObject.SetActive( true );
		_doAction = true;
		_degrees = _startDegrees;
		_doneDegreesAction = false;
	}

	void Awake()
	{
		gameObject.SetActive( false );
		_degrees = _startDegrees;
		_doAction = false;
		_doneDegreesAction = false;
	}

	void Start()
	{

	}

	void Update()
	{
		if( _doAction == false )
			return;

		float deltaDegrees = 90.0f*Time.deltaTime;
		_degrees = _degrees + deltaDegrees;

		if( _degrees >= actionDegrees && _doneDegreesAction == false && OnDegreesEvent != null )
		{
			OnDegreesEvent();
			_doneDegreesAction = true;
		}

		if( _degrees >= _endDegrees )
		{
			_doAction = false;
			_doneDegreesAction = true;
			gameObject.SetActive( false );
		}
	}
}
