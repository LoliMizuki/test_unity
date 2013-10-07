using UnityEngine;
using System.Collections.Generic;

public class KujiControl : MonoBehaviour
{
	public List<GameObject> DangosOnKujiList = new List<GameObject>();
	int _activeDangoCount = 0;
	DangoInfo.DangoType[] _currOwnDangoTypesList = new DangoInfo.DangoType[3];

	public void AddDangoWithType(DangoInfo.DangoType dangoType)
	{
		DangosOnKujiList[ _activeDangoCount ].GetComponent<exSprite>().color = DangoInfo.DangoColorByType( dangoType );
		DangosOnKujiList[ _activeDangoCount ].SetActive( true );
		_currOwnDangoTypesList[ _activeDangoCount ] = dangoType;

		_activeDangoCount++;

		if( _activeDangoCount >= DangosOnKujiList.Count )
		{
			int matchCount = 0;
			for( int i = 0; i < _currOwnDangoTypesList.Length; i++ )
			{
				if( _currOwnDangoTypesList[ i ] == GameGlobal.GetInstance().CurrTargetDangoTypesPattern[ i ] )
				{
					matchCount++;
				}
			}

			GameGlobal.GetInstance().DoDangosMatchAction( matchCount );
			SetAllDangoInactive();
		}
	}

	void Start()
	{
		SetAllDangoInactive();
	}

	void SetAllDangoInactive()
	{
		_activeDangoCount = 0;
		foreach( GameObject d in DangosOnKujiList )
			d.SetActive( false );

		for( int i = 0; i < _currOwnDangoTypesList.Length; i++ )
			_currOwnDangoTypesList[ i ] = DangoInfo.DangoType.Unkonw;
	}
}
