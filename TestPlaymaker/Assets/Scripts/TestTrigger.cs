using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if( other.gameObject.name != "PlayerBall" )
			return;

		MZDebug.Log( "Player come-in!!!!" );
	}
}
