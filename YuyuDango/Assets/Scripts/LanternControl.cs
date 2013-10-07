using UnityEngine;
using System.Collections;

public class LanternControl : MonoBehaviour
{
	const float _amplitude = 15;
	float _maxDegrees = _amplitude;
	float _minDegrees = -_amplitude;
	float _currDegrees = 0;
	float _colddown = 0;
	float _deltaDegrees = 45*20;
	int _currDir = 1;

	void Start()
	{
		_colddown = GetNextColddown();
	}

	void Update()
	{
		_colddown -= Time.deltaTime;

		if( _colddown >= 0 )
			return;
		if( _colddown < -0.5 )
		{
			_colddown = GetNextColddown();
			gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0 );
			return;
		}

		_currDegrees += _currDir*_deltaDegrees*Time.deltaTime;
		if( _currDir > 0 && _currDegrees >= _maxDegrees )
		{
			_currDir = -1;
			_currDegrees = _maxDegrees;
		}
		else if( _currDir < 0 && _currDegrees <= _minDegrees )
			{
				_currDir = 1;
				_currDegrees = _minDegrees;
			}

		gameObject.transform.rotation = Quaternion.Euler( 0, 0, _currDegrees );
	}

	float GetNextColddown()
	{
		return (float)MZMath.RandomFromRange( 200, 500 )/100.0f ;
	}
}
