
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
            if (_mesh == null) _mesh = Tetrahedron();

            if (_mat == null) _mat = Common.MaterialUnlitNext;

            if (_block == null) _block = new MaterialPropertyBlock();

            _block.SetColor("_FillColor", color);
            _block.SetFloat("_AASmoothing" , Common.AASmoothing );

            Graphics.DrawMesh(_mesh, matrix, _mat, 0, Common.camera , 0, _block);
        }

        public static Mesh Tetrahedron()
        {
            var mesh = new Mesh();

            var r = 1 / 2f; // on a 0.5 radius shpere resulting in a mesh size of 1 

            var z = r * 0.5f;
            var a = r * Mathf.Sqrt(3) / 2f;
            var y = r / 3f ;

            Vector3[] verts = new Vector3[]
            {
                new Vector3(   0,   0,   r ), // top 

                new Vector3(   0,   r,   -y ),
                new Vector3(   a, - z,   -y ),
                new Vector3( - a, - z,   -y )
            };

            int[] tri = new int[]
            {
                0,1,2, 
                0,2,3,
                1,3,1, 
                1,2,3,
            };

            mesh.SetVertices(verts);
            mesh.SetTriangles(tri, 0);
            mesh.RecalculateNormals();

            return mesh;
        }
    }

}