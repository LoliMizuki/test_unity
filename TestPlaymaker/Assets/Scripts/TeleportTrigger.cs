using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour
{
	public GameObject TeleportTarget = null;

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

		other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		other.gameObject.transform.position = TeleportTarget.transform.position;
	}
}
