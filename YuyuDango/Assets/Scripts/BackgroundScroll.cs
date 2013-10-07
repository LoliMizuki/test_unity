using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
	public Vector2 DeltaOffset = new Vector2( 100, 0 );

	void Update()
	{
		Vector2 texSize = new Vector2( gameObject.renderer.material.mainTexture.width, gameObject.renderer.material.mainTexture.height );

		Vector2 currOffset = new Vector2(
			gameObject.renderer.material.mainTextureOffset.x*texSize.x,
			gameObject.renderer.material.mainTextureOffset.y*texSize.y );

		Vector2 currDeltaOffset = DeltaOffset*Time.deltaTime;
		Vector2 nextOffset = currOffset + currDeltaOffset;

		gameObject.renderer.material.mainTextureOffset = new Vector2( nextOffset.x/texSize.x, nextOffset.y/texSize.y );
	}
}
