using UnityEngine;
using System.Collections;

public class TitleGesturesControl : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
	
	}

	void OnTap(TapGesture gesture)
	{
		if( gesture.Selection == null )
			return;

		if( gesture.Selection.name == "PlayButton" )
		{
			Application.LoadLevel( "GamePlayScene" );
		}
	}
}
