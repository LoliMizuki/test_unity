using UnityEngine;
using System.Collections;

public class GestureControl : MonoBehaviour
{
	public GameObject DragControlObject;
	Vector3 _dragControlObjectTouchStartWorldPosition;

	void OnTap(TapGesture gesture)
	{
		if( gesture.Selection == null )
			return;

		if( gesture.Selection.name == "PauseButton" )
		{
			if( Time.timeScale > 0 )
				Time.timeScale = 0;
			else
				Time.timeScale = 1.0f;
		}

		if( gesture.Selection.name == "ResetButton" )
		{
			GameGlobal.GetInstance().ResetGame();
		}
	}

	void OnDrag(DragGesture gesture)
	{
		if( DragControlObject == null )
			return;

		switch( gesture.Phase )
		{
			case ContinuousGesturePhase.Started:
				DragControlObject.SendMessage( "OnTouchBegan", gesture );
				break;

			case ContinuousGesturePhase.Updated:
				DragControlObject.SendMessage( "OnTouchMoved", gesture );
				break;

			case ContinuousGesturePhase.Ended:
				DragControlObject.SendMessage( "OnTouchEnded", gesture );
				break;

			default:
				break;
		}
	}
}
