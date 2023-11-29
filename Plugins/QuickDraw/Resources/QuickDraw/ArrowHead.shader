Shader "Hidden/Shapes/LineSegment/ArrowHead"
{
	Properties
	{
	}
	SubShader
	{
		Tags { 
			"RenderType" = "Transparent" 
			"Queue" = "Transparent+171" 
			"DisableBatching" = "true" 
		}
		LOD 100

		Pass
		{
		    ZWrite Off
		    Cull off
		    Blend SrcAlpha OneMinusSrcAlpha
		    
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 vertexDistances : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			
			UNITY_INSTANCING_BUFFER_START(CommonProps)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _AASmoothing)
				UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _BaseEdgeNoAAMinX)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _BaseEdgeNoAAMaxX)
			UNITY_INSTANCING_BUFFER_END(CommonProps)

            //float _AASmoothing;
			//fixed4 _Color;
			//float _BaseEdgeNoAAMinX;
			//float _BaseEdgeNoAAMaxX;
			
			v2f vert (appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.vertexDistances = v.color.xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);

				fixed4 v_color_ =				UNITY_ACCESS_INSTANCED_PROP(CommonProps, _Color );
				float v_AASmoothing_ =			UNITY_ACCESS_INSTANCED_PROP(CommonProps, _AASmoothing);
				float v_BaseEdgeNoAAMinX =		UNITY_ACCESS_INSTANCED_PROP(CommonProps, _BaseEdgeNoAAMinX);
				float v_BaseEdgeNoAAMaxX =		UNITY_ACCESS_INSTANCED_PROP(CommonProps, _BaseEdgeNoAAMaxX);

			    float deltas = fwidth(i.vertexDistances);
			    
			    float3 edgeAlphas = smoothstep(0,deltas* v_AASmoothing_,i.vertexDistances);
			    
			    float edgeAlpha = min(edgeAlphas.x,min(edgeAlphas.y,edgeAlphas.z));
			    
			    if(edgeAlpha == edgeAlphas.z && i.uv.x >= v_BaseEdgeNoAAMinX && i.uv.x <= v_BaseEdgeNoAAMaxX ) {
			        edgeAlpha = 1.0;
			    }
			    
			    fixed4 finalColor = v_color_;
			    finalColor.a *= edgeAlpha;
			    
				return finalColor;
			}
			ENDCG
		}
	}
}
