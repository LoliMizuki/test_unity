using UnityEngine;
using System.Collections;

// TODO:
// support:
// base on what's plane, HARD!!!!
public class MZPrimitiveGen
{
    public struct RectangleMeshData
    {
        public Vector3[] Vertices;
        public int[] TriangleOrders;
        public Vector2[] UVs;
    }

    static public RectangleMeshData GenRectangleDataAtOrigin()
    {
        RectangleMeshData data = new RectangleMeshData();

        data.Vertices = GenVertices( Vector2.zero, new Vector2( 1, 1 ) );
        data.TriangleOrders = GenTriangleOreders( 0 );
        data.UVs = GenUVs( new Vector2( 1, 1 ), new Vector2( 1, 1 ) );

        return data;
    }

    static public Vector3[] GenVertices(Vector2 offset, Vector2 size)
    {
        float pos_x = size.x*0.5f;
        float pos_y = size.y*0.5f;
        float pos_z = 0;

        Vector3[] verts = new Vector3[4];
        
        verts[ 0 ] = new Vector3( offset.x + pos_x, offset.y + pos_y, pos_z );
        verts[ 1 ] = new Vector3( offset.x + pos_x, offset.y - pos_y, pos_z );
        verts[ 2 ] = new Vector3( offset.x - pos_x, offset.y + pos_y, pos_z );
        verts[ 3 ] = new Vector3( offset.x - pos_x, offset.y - pos_y, pos_z );

        return verts;
    }

    static public int[] GenTriangleOreders(int triangleStart)
    {
        int[] triangleOreders = new int[6];
        triangleOreders[ 0 ] = triangleStart + 0;
        triangleOreders[ 1 ] = triangleStart + 1;
        triangleOreders[ 2 ] = triangleStart + 2;
        triangleOreders[ 3 ] = triangleStart + 2;
        triangleOreders[ 4 ] = triangleStart + 1;
        triangleOreders[ 5 ] = triangleStart + 3;

        return triangleOreders;
    }

    static public Vector2[] GenUVs(Vector2 topRight, Vector2 size)
    {
        Vector2[] uvs = new Vector2[4];
//        uvs[ 0 ] = new Vector2( 1, 1 );
//        uvs[ 1 ] = new Vector2( 1, 0 );
//        uvs[ 2 ] = new Vector2( 0, 1 );
//        uvs[ 3 ] = new Vector2( 0, 0 );

        uvs[ 0 ] = new Vector2( topRight.x, topRight.y );
        uvs[ 1 ] = new Vector2( topRight.x, topRight.y - size.y );
        uvs[ 2 ] = new Vector2( topRight.x - size.x, topRight.y );
        uvs[ 3 ] = new Vector2( topRight.x - size.x, topRight.y - size.y );

        return uvs;
    }

    static public void ApplyMesh(Mesh mesh, Vector3[] vertices, int[] triangles, Vector2[] uvs)
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        if( uvs != null && uvs.Length != 0 )
            mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}
