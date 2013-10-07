using UnityEngine;
using System.Collections;

public class ScaleUpDown : MonoBehaviour
{
	public float MaxScale;
	public float MinScale;
	public float deltaScale;
	exSprite _exSprite;
	int _currScaleDirection;

	void Awake()
	{
		_exSprite = GetComponent<exSprite>();
	}

	void Start()
	{
		MaxScale = 0.7f;
		MinScale = 0.4f;
		deltaScale = 0.8f;
		_currScaleDirection = 1;
	}

	void Update()
	{
		float currScale = _exSprite.scale.x;
		currScale += _currScaleDirection*deltaScale*Time.deltaTime;

		if( currScale >= MaxScale )
		{
			_currScaleDirection = -1;
			currScale = MaxScale;
		}

		if( currScale <= MinScale )
		{
			_currScaleDirection = 1;
			currScale = MinScale;
		}

		_exSprite.scale = new Vector2( currScale, currScale );
	}
}
