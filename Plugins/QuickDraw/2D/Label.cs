﻿
using UnityEngine;

namespace QuickDraw
{
    public static class Label
    {
        // https://www.branah.com/unicode-converter

        public static char UnicodeDegree = '\u00b0'; // °
        public static char UnicodeAlpha = '\u03b1'; // α
        public static char UnicodeDelta = '\u03b4'; // δ
        public static char UnicodeTriangleup = '\u25b2'; // ▲

        public static string FormatFloat( float x ) => $"{( x >= 0 ? "+" : "")}{x:N2}";
        public static string FormatDegrees( float x ) => FormatFloat( x ) + UnicodeDegree;

        public static string WinAltChars = " ☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼ !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥₧ƒáíóúñÑªº¿⌐¬½¼¡«»░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌█▄▌▐▀αßΓπΣσµτΦΘΩδ∞φε∩≡±≥≤⌠⌡÷≈°∙·√ⁿ²■ ";

        static Texture2D _tex;
        static Mesh _mesh;
        static Material _mat;
        static MaterialPropertyBlock _block;

        public static string outVal = "";

        public static void Draw( Vector3 position, string label, Color color, float size = 0.1f, bool center = true )
        {
            if( label == null || string.IsNullOrEmpty( label.Trim() ) ) return;

            if (_mesh == null) _mesh = Quad();

            if( _tex == null ) _tex = Resources.Load<Texture2D>( "QuickDraw/unifont_8x16" );

            if (_mat == null ) 
            {
                _mat = new Material(Shader.Find("Hidden/Shapes/Label"));
                
                _mat.enableInstancing = true;

                //_mat.SetTexture("_MainTex", _tex );
                //_mat.SetFloat("_AASmoothing", Common.AASmoothing);

                Shader.SetGlobalTexture("QuickDraw_LabelTex", _tex);
            }

            if ( _block == null ) _block = new MaterialPropertyBlock();

            while( label.Length < 3 ) label += ' ';

            var rotation = Quaternion.identity; // Quaternion.LookRotation(Common.normal);

            if ( label.Length > 3 )
            {
                int count = Mathf.CeilToInt( ( label.Length - 1 ) / 3 );
                var step = ( 3/4f ) * size;
                var stepSize = rotation * ( step * Vector3.right );

                if( center )
                {
                    Draw( position + stepSize * ( count * ( 1 ) ), label.Substring(0, 3) , color, size, center );
                    Draw( position - stepSize * ( 1 ), label.Substring( 3 ), color , size, center);
                }
                else
                {
                    Draw( position , label.Substring(0, 3), color, size, center );
                    Draw( position - stepSize * 2f , label.Substring(3), color, size, center );
                }

                return;
            }

            int data = 0;

            for( var i = 0 ; i < 3 ; ++ i ) data |= ( ( WinAltChars.IndexOf( label[ i ] ) + 1 ) << ( 8 * i ) );

            _block.SetColor("_FillColor", color);
            _block.SetInt("_index", data );

            var scale = new Vector3( 3f/4f, 0.5f, 0 ) * size;

            var m = Matrix4x4.TRS( position, Quaternion.LookRotation( Common.normal ), scale );

            Common.Render(_mesh, m, _mat, _block);
        }

        public static Mesh Quad()
		{
			var mesh = new Mesh();

			mesh.SetVertices(new Vector3[]
			{
				new Vector3( -1, -1,  0 ),
				new Vector3(  1, -1,  0 ),
				new Vector3(  1,  1,  0 ),
				new Vector3( -1,  1,  0 )
			});

			mesh.triangles = new[] { 0, 2, 1, 0, 3, 2 };

			mesh.uv = new[]
			{
				new Vector2( -1, -1 ),
				new Vector2(  1, -1 ),
				new Vector2(  1,  1 ),
				new Vector2( -1,  1 )
			};

			return mesh;
		}


    }
}