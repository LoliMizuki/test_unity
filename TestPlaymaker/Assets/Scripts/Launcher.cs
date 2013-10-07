using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
	public GameObject Bullet = null;
	Rigidbody _bulletRigidbody = null;

	public void Launch()
	{
		Bullet.SetActive( true );
		Bullet.transform.position = transform.position + new Vector3( 20, 0, 0 );

		_bulletRigidbody.angularVelocity = Vector3.zero;
		_bulletRigidbody.velocity = Vector3.zero;
		_bulletRigidbody.AddForce( new Vector3( 50000*_bulletRigidbody.mass, 0, 0 ) );
	}

	void Awake()
	{
		MZDebug.Assert( Bullet != null, "Bullet is null" );
		MZDebug.Assert( Bullet.GetComponent<Rigidbody>() != null, "Bullet not have RigidBody" );

		Bullet.SetActive( false );
		_bulletRigidbody = Bullet.GetComponent<Rigidbody>();
	}

	void Start()
	{

	}

	void Update()
	{
	
	}
}
