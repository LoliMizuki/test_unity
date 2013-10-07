using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public void GoGoRotate()
	{
		enabled = true;
	}

	void Awake()
	{
		enabled = false;
	}

	void Start()
	{

	}

	void Update()
	{
		transform.Rotate( new Vector3( 0, 0, -100*Time.deltaTime ) );
	}
}
