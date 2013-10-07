using UnityEngine;
using System.Collections;

public class YuyukoBodyControl : MonoBehaviour
{
	public GameObject GauzeObject;
	Vector2 _gauzeObjectMoveDirection;

	public void DoHitten(Vector3 hitterPosition)
	{
		this.gameObject.GetComponent<exSprite>().spanim.Play( "yuyuko_hit" );
		this.gameObject.transform.parent.GetComponent<YuyukoControl>().DisableUserControl = true;

		GameGlobal.GetInstance().DoDangoHitYuyukoBody();

		if( GauzeObject != null )
		{
			GauzeObject.SetActive( true );

			Vector3 vecFromHitToBoby = MZMath.UnitVectorFromP1ToP2( hitterPosition, transform.position );
			vecFromHitToBoby = vecFromHitToBoby*20;

			GauzeObject.transform.position =
				new Vector3(
					hitterPosition.x + vecFromHitToBoby.x,
					hitterPosition.y + vecFromHitToBoby.y, GauzeObject.transform.position.z );
		}
	}

	public void OnHitStateEnd()
	{
		this.gameObject.GetComponent<exSprite>().spanim.Play( "yuyuko_normal" );
		this.gameObject.transform.parent.GetComponent<YuyukoControl>().DisableUserControl = false;

		if( GauzeObject != null )
			GauzeObject.SetActive( false );
	}

	void Start()
	{

	}

	void Update()
	{
//		if( GauzeObject.activeSelf == false )
//			return;
//
//		GauzeObject.transform.localRotation += new Quaternion( 0, 0, 40*Time.deltaTime );
//		GauzeObject.transform.localRotation += new Quaternion( 0, 0, 40*Time.deltaTime );
	}
}
