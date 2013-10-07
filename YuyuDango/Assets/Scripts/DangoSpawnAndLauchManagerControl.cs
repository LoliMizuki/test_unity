using UnityEngine;
using System;
using System.Collections.Generic;

public class DangoSpawnAndLauchManagerControl : MonoBehaviour
{
	public GameObject DangoOrigin;
	public GameObject DangoLayer;
	public float InitLaunchInterval = 2.0f;
	public int InitNumberOfSpawnPerLauncher = 1;
	float _launcherTimeCount;
	List<DangoControl> _dangoControlsList;

	void Start()
	{
		_launcherTimeCount = 0;

		_dangoControlsList = new List<DangoControl>();
		for( int i = 0; i < 30; i++ )
		{
			GameObject dango = (GameObject)GameObject.Instantiate( DangoOrigin );
			dango.transform.position = new Vector3( -999, -999, 0 );
			dango.transform.parent = DangoLayer.transform;
			dango.SetActive( false );
			_dangoControlsList.Add( dango.GetComponent<DangoControl>() );
		}
	}

	void Update()
	{
		UpdateLaunchers();
	}

	void UpdateLaunchers()
	{
		_launcherTimeCount -= Time.deltaTime;
		if( _launcherTimeCount > 0 )
			return;

		_launcherTimeCount += InitLaunchInterval; // next launch time
		// curr launcher number
		for( int i = 0; i < InitNumberOfSpawnPerLauncher; i++ )
		{
			LaunchDango();
		}
	}

	void OnDrawGizmos()
	{

	}

	void LaunchDango()
	{
		Func<Vector3> GetLaunchPosition = () =>
		{
			int choiceSide = MZMath.RandomFromRange( 0, 3 );
			Rect dangoSpawnRange = GameGlobal.GetInstance().DangoSpawnRange;

			switch( choiceSide )
			{
				case 0:
					return new Vector3( dangoSpawnRange.x + (float)MZMath.RandomFromRange( 0, (int)dangoSpawnRange.width ),
						dangoSpawnRange.y,
						0 );
				case 1:
					return new Vector3( dangoSpawnRange.x + (float)MZMath.RandomFromRange( 0, (int)dangoSpawnRange.width ),
						dangoSpawnRange.y + dangoSpawnRange.height,
						0 );

				case 2:
					return new Vector3( dangoSpawnRange.x,
						dangoSpawnRange.y + (float)MZMath.RandomFromRange( 0, (int)dangoSpawnRange.height ),
						0 );

				case 3:
					return new Vector3( dangoSpawnRange.x + dangoSpawnRange.width,
						dangoSpawnRange.y + (float)MZMath.RandomFromRange( 0, (int)dangoSpawnRange.height ),
						0 );
			}

			return Vector3.zero;
		};

		DangoControl nextDc = GetInactiveDangoControl();
		nextDc.Spawn( GetLaunchPosition() );
	}

	DangoControl GetInactiveDangoControl()
	{
		MZDebug.Assert( _dangoControlsList != null && _dangoControlsList.Count > 0, "Where is _dangoControlsList?" );

		foreach( DangoControl dc in _dangoControlsList )
		{
			if( dc.IsActive == false )
				return dc;
		}

		return null;
	}
}
