using UnityEngine;
using System.Collections.Generic;

public class TargetDangosControl : MonoBehaviour
{
	public List<GameObject> DangonsList;

	public void SetTargetDangoPattern(DangoInfo.DangoType[] targetDangoTypesList)
	{
		if( DangonsList == null || DangonsList.Count == 0 )
			return;

		int i = 0;
		foreach( DangoInfo.DangoType type in targetDangoTypesList )
		{
			DangonsList[ i ].GetComponent<exSprite>().color = DangoInfo.DangoColorByType( type );
			i++;
		}
	}
}
