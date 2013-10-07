using UnityEngine;
using System.Collections;

public class GUIControl : MonoBehaviour
{
	Camera _camera;

	void Awake()
	{
		_camera = Camera.mainCamera;
	}

	void Start()
	{

	}

	void Update()
	{
		this.gameObject.transform.position = _camera.transform.position;	
	}
}
