using UnityEngine;
using System;
using System.Collections.Generic;

public class CubeGen : MonoBehaviour
{
    delegate Vector3 GetVertexWithHalfSizeAndSign(float halfSize ,Vector2 sign);
    
    Mesh _mesh;
    MeshRenderer _meshRender;
    List<Vector3> _vertices;
    List<int> _triangles;
    List<Vector2> _texUVs;

    void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        
        _mesh = gameObject.GetComponent<MeshFilter>().mesh;
        _meshRender = gameObject.GetComponent<MeshRenderer>();
        
        InitCubeMesh();
  
        ApplyMesh();        
        
        transform.localScale = ( new Vector3( 1, 1, 1 ) )*100;
    }
    
    void InitCubeMesh()
    {
        float half_size = 0.5f;
        Vector2[] sign_order = { new Vector2( 1, 1 ), new Vector2( 1, -1 ), new Vector2( -1, 1 ), new Vector2( -1, -1 ) };
        
        // front
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( halfSize*sign.x, halfSize*sign.y, -halfSize ); 
        } );
        
        // back        
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( -halfSize*sign.x, halfSize*sign.y, halfSize ); 
        } );
        
        // left
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( halfSize, halfSize*sign.y, halfSize*sign.x ); 
        } );
        
        // right
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( -halfSize, halfSize*sign.y, -halfSize*sign.x ); 
        } );
        
        // top
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( halfSize*sign.x, halfSize, halfSize*sign.y ); 
        } );
        
        // bottom
        AddVerticesAndSetTriangleOrderAndUV( sign_order, half_size, (halfSize, sign) => 
        {
            return new Vector3( halfSize*sign.x, -halfSize, -halfSize*sign.y ); 
        } );
    }
    
    void AddVerticesAndSetTriangleOrderAndUV(Vector2[] signOrder, float halfSize, GetVertexWithHalfSizeAndSign getVertex)
    {
        int startIndex = ( _vertices != null )? _vertices.Count : 0;
        
        AddVertice( signOrder, halfSize, getVertex );
        AddTriangleOrder( startIndex );
        AddOneUVSet();
    }
    
    void AddVertice(Vector2[] signOrder, float halfSize, GetVertexWithHalfSizeAndSign getVertex)
    {
        if( _vertices == null )
            _vertices = new List<Vector3>();
    
        foreach( Vector2 sign in signOrder )
        {
            _vertices.Add( getVertex( halfSize, sign ) );
        }
    }
    
    void AddTriangleOrder(int startIndex)
    {
        if( _triangles == null )
            _triangles = new List<int>();
    
        int[] order = { startIndex, startIndex + 1, startIndex + 2,
                            startIndex + 2, startIndex + 1, startIndex + 3 };
        _triangles.AddRange( order );    
    }
    
    void AddOneUVSet()
    {
        if( _texUVs == null )
            _texUVs = new List<Vector2>();
      
        _texUVs.Add( new Vector2( 1, 1 ) );
        _texUVs.Add( new Vector2( 1, 0 ) );
        _texUVs.Add( new Vector2( 0, 1 ) );
        _texUVs.Add( new Vector2( 0, 0 ) );
    }
    
    void ApplyMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.uv = _texUVs.ToArray();
        _mesh.Optimize();
        _mesh.RecalculateNormals();
        
        _meshRender.material.mainTexture = Resources.LoadAssetAtPath( "Assets/Textures/Sachiko.jpg", typeof( Texture ) ) as Texture2D;
        _meshRender.material.shader = Shader.Find( "Unlit/Texture" );
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube( Vector2.zero, new Vector3( 100, 100, 100 ) );
    }
}
