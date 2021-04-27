
using UnityEngine;

namespace QuickDraw
{
    public class Pyramid
    {
        static Mesh _mesh;
        static Material _mat;
        static MaterialPropertyBlock _block;

        public static void Draw(Matrix4x4 matrix, Color color)
        {
            if (_mesh == null) _mesh = Octahedron();

            if (_mat == null) _mat = Common.MaterialUnlitNext;

            if (_block == null) _block = new MaterialPropertyBlock();

            _block.SetColor("_FillColor", color);
            _block.SetFloat("_AASmoothing" , 1.5f );

            Graphics.DrawMesh(_mesh, matrix, _mat, 0, Common.camera , 0, _block);
        }

        public static Mesh Octahedron()
        {
            var mesh = new Mesh();

            var r = 1 / 2f; // on a 0.5 radius shpere resulting in a mesh size of 1 

            Vector3[] verts = new Vector3[]
            {
                new Vector3(   0,   r ,   0 ), // top 

                new Vector3(   0,   0 ,   r ), // base
                new Vector3(   r,   0 ,   0 ),
                new Vector3(   0,   0 , - r ),
                new Vector3( - r,   0 ,   0 ),

                new Vector3(   0, - r ,   0 ), // bottom 
            };

            //      1
            //    /   \
            //  4       2
            //    \   /
            //      3

            int[] tri = new int[]
            {
                1,2,0, 2,3,0, 3,4,0, 4,1,0,
                1,4,5, 4,3,5, 3,2,5, 2,1,5,
            };

            mesh.SetVertices(verts);
            mesh.SetTriangles(tri, 0);
            mesh.RecalculateNormals();

            return mesh;
        }
    }

}