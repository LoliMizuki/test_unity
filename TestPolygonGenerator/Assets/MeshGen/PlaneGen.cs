using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneGen : MonoBehaviour
{
//    float _currentY;

    void Awake()
    {
        AddGameObjectComponenet();

        
        ApplyMesh();
//        transform.localScale = new Vector3( 214/4, 300/4, 0 );
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    void AddGameObjectComponenet()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
    }

    void ApplyMesh()
    {
        MZPrimitiveGen.RectangleMeshData meshData = MZPrimitiveGen.GenRectangleDataAtOrigin();

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MZPrimitiveGen.ApplyMesh( mesh, meshData.Vertices, meshData.TriangleOrders, meshData.UVs );

        MeshRenderer meshRender = GetComponent<MeshRenderer>();
        meshRender.material.mainTexture = Resources.LoadAssetAtPath( "Assets/Textures/Sachiko.jpg", typeof( Texture ) ) as Texture2D;
        meshRender.material.shader = Shader.Find( "Unlit/Texture" );
    }
}
