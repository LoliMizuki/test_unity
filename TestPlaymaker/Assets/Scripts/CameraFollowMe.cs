using UnityEngine;
using System.Collections;

public class CameraFollowMe : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		Camera camera = Camera.main.camera;
		camera.transform.position = new Vector3( transform.position.x, transform.position.y, camera.transform.position.z );
	}
}
