using UnityEngine;
using System.Collections.Generic;

public class TriangleMeshGen : MonoBehaviour
{
    List<Vector3> _vertices;
    List<int> _triangles;
    List<Vector2> _texUVs;

    void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        
        _vertices = new List<Vector3>();
        _vertices.Add( new Vector3( 0, 40, 0 ) );
        _vertices.Add( new Vector3( 10, 0, -10 ) );
        _vertices.Add( new Vector3( -10, 0, -10 ) );
        _vertices.Add( new Vector3( 0, 0, 10 ) );
        
        _triangles = new List<int>();
        
        // front
        _triangles.Add( 0 );
        _triangles.Add( 1 );
        _triangles.Add( 2 );
        
        // left
        _triangles.Add( 0 );
        _triangles.Add( 2 );
        _triangles.Add( 3 );
        
        // right
        _triangles.Add( 0 );
        _triangles.Add( 1 );
        _triangles.Add( 3 );
        
        // button
        _triangles.Add( 3 );
        _triangles.Add( 2 );
        _triangles.Add( 1 );
        
        // UV
        _texUVs = new List<Vector2>();
        _texUVs.Add( new Vector2( 0, 0 ) );
        _texUVs.Add( new Vector2( 1, 0 ) );
        _texUVs.Add( new Vector2( 0, 1 ) );
        _texUVs.Add( new Vector2( 1, 1 ) );
        
        ApplyMesh();
    }
 
    void Start()
    {
 
    }
 
    void Update()
    {
 
    }
 
    void ApplyMesh()
    {
        MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
        Texture2D t =  Resources.LoadAssetAtPath( "Assets/Textures/sachiko.JPG", typeof(Texture) ) as Texture2D;
        if( t == null )
        {
            Debug.Log( "no texture" );
            Debug.Break();
        }
        meshRender.material.mainTexture = t;

    
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.uv = _texUVs.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();      
    }
}
