using UnityEngine;
using System.Collections.Generic;

public class GridGen : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        MZPrimitiveGen.ApplyMesh(
                                mesh,
                                GenMeshVertices(),
                                GenTriangleOrders(),
                                GenUVs() );

        MeshRenderer meshRender = GetComponent<MeshRenderer>();
        meshRender.material.mainTexture = Resources.LoadAssetAtPath( "Assets/Textures/kobato.jpg", typeof( Texture ) ) as Texture2D;
        meshRender.material.shader = Shader.Find( "Unlit/Texture" );
    }

    Vector3[] GenMeshVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        // from right-top
        for( int x = 0; x < 2; x++ )
        {
            Vector3[] currOffset = MZPrimitiveGen.GenVertices( new Vector3( 0.5f - x, 0, 0 ), new Vector2( 1, 1 ) );
            vertices.AddRange( currOffset );
        }

        return vertices.ToArray();
    }

    int[] GenTriangleOrders()
    {
        List<int> triangleOrders = new List<int>();

        triangleOrders.AddRange( MZPrimitiveGen.GenTriangleOreders( 0 ) );
        triangleOrders.AddRange( MZPrimitiveGen.GenTriangleOreders( 4 ) );

        return triangleOrders.ToArray();
    }

    Vector2[] GenUVs()
    {
        List<Vector2> uvs = new List<Vector2>();
        for( int i = 0; i < 2; i++ )
        {
            uvs.AddRange( MZPrimitiveGen.GenUVs( new Vector2( 1 - 0.5f*i, 1 ), new Vector2( 0.5f, 1.0f ) ) );
        }

        return uvs.ToArray();
    }
}
