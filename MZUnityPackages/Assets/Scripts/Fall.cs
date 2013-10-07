using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour
{
	void Start()
	{
		GetComponent<Rigidbody>().AddForce( new Vector3( 0, -100, 0 ) );
	}

	void Update()
	{
//		transform.position += new Vector3( 0, -10*Time.deltaTime, 0 );
	}
}
