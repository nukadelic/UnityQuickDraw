using System.Collections;
using UnityEngine;
using QuickDraw;

public static class Draw 
{
    public static Matrix4x4 matrix = Matrix4x4.identity;

    public static void label( Vector3 position, string label, Color color, float size = 0.1f, bool center = true )
    {
        Label.Draw( matrix.MultiplyPoint( position ), label, color, size, center );
    }

    public static void circle( Vector3 position, float radius, Color color )
    {
		Circle.Draw( new Circle.Info {
			/*float*/ radius = radius, /*Vector3*/ center = matrix.MultiplyPoint( position ), /*Vector3*/ forward = Common.normal, /*Color*/ fillColor = color,
			/*bool*/ bordered = false, /*Color*/ borderColor = Common.NoColor, /*float*/ borderWidth = 0, 
			/*bool*/ isSector = false, /*float*/ sectorInitialAngleInDegrees = 0, /*float*/ sectorArcLengthInDegrees = 0,
		} );
    }

    public static void circle( Vector3 position, float radius, Vector3 normal, Color borderColor, float borderRatio = 0.1f )
    {
        Circle.Draw( new Circle.Info {
			/*float*/ radius = radius, /*Vector3*/ center = matrix.MultiplyPoint( position ), /*Vector3*/ forward = normal, /*Color*/ fillColor = Common.NoColor,
			/*bool*/ bordered = true, /*Color*/ borderColor = borderColor, /*float*/ borderWidth = borderRatio * radius, 
			/*bool*/ isSector = false, /*float*/ sectorInitialAngleInDegrees = 0, /*float*/ sectorArcLengthInDegrees = 0,
		} );
    }


    public static void circle(Vector3 position, float radius, Color color, Color borderColor, float borderRatio = 0.1f )
    {
		Circle.Draw( new Circle.Info {
			/*float*/ radius = radius, /*Vector3*/ center = matrix.MultiplyPoint( position ), /*Vector3*/ forward = Common.normal, /*Color*/ fillColor = color,
			/*bool*/ bordered = true, /*Color*/ borderColor = borderColor, /*float*/ borderWidth = borderRatio * radius, 
			/*bool*/ isSector = false, /*float*/ sectorInitialAngleInDegrees = 0, /*float*/ sectorArcLengthInDegrees = 0,
		} );
    }
    public static void circle2(Circle.Info info) => Circle.Draw(info);

    public static void point(Vector3 pos, Color color, float size = 0.05f)
    {
        var m = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one * size);

        Point.Draw( matrix * m, color );
    }

    public static void pyramid(Vector3 pos, Vector3 normal, Color color, float size = 0.05f)
    {
        pyramid(pos, normal, color, Vector3.one * size);
    }
    public static void pyramid(Vector3 pos, Vector3 normal, Color color, Vector3 size)
    {
        var m = Matrix4x4.TRS(pos, Quaternion.LookRotation( normal ), size);

        Pyramid.Draw( matrix * m, color );
    }

    public static void rod(Vector3 a, Vector3 b, Color color, float thickness = 0.01f)
    {
        var p = (a + b) / 2f;
        var r = Quaternion.FromToRotation(Vector3.up, b - a);
        var s = Vector3.one * thickness;
        s.y = (b - a).magnitude;
        var m = Matrix4x4.TRS(p, r, s);

        Rod.Draw( matrix * m, color );
    }

    public static void line( Vector3 a, Vector3 b, Color color, float thickness = 0.01f )
    {
        LineSegment.Draw( new LineSegment.Info {
            /*Vector3*/ startPos = matrix.MultiplyPoint( a ), /*Vector3*/ endPos = matrix.MultiplyPoint( b ), 
            /*Color*/ fillColor = color, /*Vector3*/ forward = Common.normal, /*float*/ width = thickness,
            /*bool*/ bordered = false, /*Color*/ borderColor = Common.NoColor, /*float*/ borderWidth = 0,
            /*bool*/ dashed = false, /*float*/ distanceBetweenDashes = 0, /*float*/ dashLength = 0,
            /*bool*/ startArrow = false, /*bool*/ endArrow = false, /*float*/ arrowWidth = 0, /*float*/ arrowLength = 0,
        } );
    }
}