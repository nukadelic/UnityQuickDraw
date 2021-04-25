
using System.Collections.Generic;
using UnityEngine;

namespace QuickDraw.Demo
{
    public class DemoMonoDraw : MonoDraw
    {
        public static float Phi = Mathf.PI * ( 3f - Mathf.Sqrt( 5f ) );
        static float Pi2 = Mathf.PI * 2;

        [Range(20,500)]
        public int count = 100;
        [Range(0.5f,2f)]
        public float radius = 1f;
        [Range(0, 1)]
        public float min = 0f;
        [Range(0,1)]
        public float max = 1f;
        [Range(0,360)]
        public float angleStartDeg = 0f;
        [Range(0, 360)]
        public float angleRangeDeg = 360;

        [Header("Draw")]
        public bool drawLabels = true;
        public bool drawCircle = false;
        public bool drawPoint = false;
        public bool drawRods = false;
        public bool drawLines = false;

        List<Vector3> points;
        
        public override void OnDraw()
        {
            if( min > max ) max = min;

            Calculate();

            Draw.matrix = transform.localToWorldMatrix;

            for ( var i = 0; i < points.Count; ++i )
            {
                if( drawCircle ) Draw.circle( points[ i ] , 0.05f , Color.white, Color.black, 0.2f );

                if( drawPoint ) Draw.point( points[ i ], Color.yellow );

                if( drawLabels ) Draw.label( points[ i ], i.ToString(), Color.white );

                if (i > 0 && drawRods) Draw.rod(points[i], points[ i - 1 ], Color.red);

                if (i > 0 && drawLines) Draw.line(points[i], points[ i - 1 ], Color.red);
            }
        }

        void Calculate()
        {
            points = new List<Vector3>();
            for (var i = 0; i < count; ++i)
            {
                var y = ((i / (count - 1f)) * (max - min) + min) * 2f - 1f; // y goes from min (-) to max (+)
                // based on : https://stackoverflow.com/a/26127012/2496170
                var rY = Mathf.Sqrt(1 - y * y); // radius at y
                var theta = Phi * i; // golden angle increment
                if (angleStartDeg != 0 || angleRangeDeg != 360)
                {
                    theta = (theta % (Pi2));
                    theta = theta < 0 ? theta + Pi2 : theta;
                    var a1 = angleStartDeg * Mathf.Deg2Rad;
                    var a2 = angleRangeDeg * Mathf.Deg2Rad;
                    theta = theta * a2 / Pi2 + a1;
                }

                var x = Mathf.Cos(theta) * rY;
                var z = Mathf.Sin(theta) * rY;
                var p = new Vector3(x, y, z) * radius;
                points.Add(p);
            }
        }

    }
}
