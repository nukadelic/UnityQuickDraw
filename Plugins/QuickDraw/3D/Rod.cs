using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickDraw
{

    public class Rod
    {
        static Mesh _mesh;
        static Material _mat;
        static MaterialPropertyBlock _block;

        public static void Draw( Matrix4x4 matrix, Color color )
        {
            if( _mesh == null ) _mesh = TriangularRod();

            if ( _mat == null ) _mat = Common.MaterialUnlitNext;

            if ( _block == null ) _block = new MaterialPropertyBlock();

            _block.SetColor( "_FillColor", color );
            _block.SetFloat("_AASmoothing", Common.AASmoothing );

            Common.Render(_mesh, matrix, _mat, _block);
        }

        public static Mesh TriangularRod()
        {
            var mesh = new Mesh();

            var r = 1 / 2f; // on a 0.5 radius shpere resulting in a mesh size of 1 

            var z = r * 0.5f;
            var a = r * Mathf.Sqrt(3) / 2f;

            Vector3[] verts = new Vector3[]
            {
                new Vector3(   0, - r ,   r ),
                new Vector3(   a, - r , - z ),
                new Vector3( - a, - r , - z ),

                new Vector3(   0,   r ,   r ),
                new Vector3(   a,   r , - z ),
                new Vector3( - a,   r , - z ),

            };

            //     0       3
            //    / \     / \
            //   2___1   5___4

            int[] tri = new int[]
            {
                0, 2, 1, // Top    - CW
                3, 4, 5, // Bottom - CCW
                2, 5, 4,   4, 1, 2, // Side Quad 1 - CW
                0, 3, 5,   5, 2, 0, // Side Quad 2 - CW
                1, 4, 3,   3, 0, 1, // Side Quad 3 - CW
            };

            mesh.SetVertices(verts);
            mesh.SetTriangles(tri, 0);
            mesh.RecalculateNormals();

            return mesh;
        }
    }

}