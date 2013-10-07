using UnityEngine;
using System;
using System.Collections;

public interface IMZTouchControl
{
	void OnTouchBegan(Gesture gesture);

	void OnTouchMoved(Gesture gesture);

	void OnTouchEnded(Gesture gesture);
}

public class YuyukoControl : MonoBehaviour, IMZTouchControl
{
	public GameObject BodyObject;
	public GameObject BodyShadowObject;
	public GameObject KujiObject;
	public bool DisableUserControl = false;

	#region - move
	Vector2 _touchBeganPositionXY;
	Gesture _currGesture = null;
	#endregion

	#region - Kuji
	Vector3 _prePositionForKuji;
	#endregion

	#region - Flip
	Vector3 _prePositionForFlip;
	#endregion

	#region - ITouchControl

	public void OnTouchBegan(Gesture gesture)
	{
		_touchBeganPositionXY = new Vector2( this.gameObject.transform.position.x, this.gameObject.transform.position.y );
		_currGesture = gesture;
	}

	public void OnTouchMoved(Gesture gesture)
	{
		_currGesture = gesture;
	}

	public void OnTouchEnded(Gesture gesture)
	{
		if( DisableUserControl == true )
			return;

		_currGesture = null;
	}

	#endregion

	public void SetNextPosition(Vector3 nextPosition)
	{
		this.gameObject.transform.localPosition = ModifyNextPositionWithBoundary( nextPosition );
	}

	void Start()
	{

	}

	void Update()
	{
		UpdateMove();
		UpdateKuji();
		UpdateFlip();
	}

	void UpdateMove()
	{
		if( _currGesture == null || DisableUserControl == true )
			return;

		float zPlane = this.gameObject.transform.parent.transform.position.z;

		Vector3 gestureStartPositionInWorld =
					Camera.mainCamera.ScreenToWorldPoint( new Vector3( _currGesture.StartPosition.x, _currGesture.StartPosition.y, zPlane ) );

		Vector3 gestureCurrPositionInWorld =
					Camera.mainCamera.ScreenToWorldPoint( new Vector3( _currGesture.Position.x, _currGesture.Position.y, zPlane ) );

		Vector3 deltaMoveInWorld = gestureCurrPositionInWorld - gestureStartPositionInWorld;

		Vector3 nextPosition = new Vector3( _touchBeganPositionXY.x + deltaMoveInWorld.x, _touchBeganPositionXY.y + deltaMoveInWorld.y, 0 );

		SetNextPosition( nextPosition );
	}

	void UpdateKuji()
	{
		if( KujiObject == null )
			return;

		// min distance
		if( Vector3.Distance( this.transform.position, _prePositionForKuji ) < 10 )
			return;

		// update body
		Vector2 moveDirection = this.transform.position - _prePositionForKuji;

		_prePositionForKuji = this.transform.position;

		float newRotationDegrees = MZMath.DegreesFromXAxisToVector( moveDirection ) + 180;
		KujiObject.transform.rotation = Quaternion.Euler( 0, 0, (float)(int)newRotationDegrees );
	}

	void UpdateFlip()
	{
		if( _currGesture == null || BodyObject == null )
			return;

		if( Vector3.Distance( _prePositionForFlip, this.gameObject.transform.position ) < 10 )
			return;

		exSprite sprite = BodyObject.GetComponent<exSprite>();
		float scaleY = sprite.scale.y;
		float absScaleX = Mathf.Abs( sprite.scale.x );
		bool isLeft = ( this.gameObject.transform.position.x > _prePositionForFlip.x );

		sprite.scale = new Vector2( -absScaleX*( ( isLeft )? -1 : 1 ), scaleY );
		_prePositionForFlip = this.gameObject.transform.position;

		if( BodyShadowObject != null )
		{
			BodyShadowObject.GetComponent<exSprite>().scale = sprite.scale;
		}
	}

	Vector3 ModifyNextPositionWithBoundary(Vector3 nextPosition)
	{
		float nextX = nextPosition.x;
		float nextY = nextPosition.y;

		Func<bool, float, float, float> CheckAndSet = (bool isLargeThan, float v, float b) =>
		{
			if( isLargeThan )
			{
				return ( v > b )? b : v;
			}
			else
			{
				return ( v < b )? b : v;
			}
		};


		Rect movableBoundary = GameGlobal.GetInstance().PlayerMovableBoundary;

		nextX = CheckAndSet( false, nextX, movableBoundary.x );
		nextY = CheckAndSet( false, nextY, movableBoundary.y );
		nextX = CheckAndSet( true, nextX, movableBoundary.x + movableBoundary.width );
		nextY = CheckAndSet( true, nextY, movableBoundary.y + movableBoundary.height );

		return new Vector3( nextX, nextY, 0 );
	}
}
