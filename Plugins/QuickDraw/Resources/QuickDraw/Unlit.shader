Shader "Hidden/Shapes/Unlit"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "DisableBatching" = "true" }
        LOD 100

        Pass
        {
            ZWrite Off
            Cull off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(CommonProps)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _FillColor)
                UNITY_DEFINE_INSTANCED_PROP(float, _AASmoothing)
            UNITY_INSTANCING_BUFFER_END(CommonProps)

            sampler2D _MainTex;
            float4 _MainTex_ST;


            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                float aaSmoothing = UNITY_ACCESS_INSTANCED_PROP(CommonProps, _AASmoothing);
                fixed4 fillColor = UNITY_ACCESS_INSTANCED_PROP(CommonProps, _FillColor);

                float distanceToCenter = length(i.uv);

                float distancePerPixel = fwidth(distanceToCenter);
                float distanceAlphaFactor = 1.0 - smoothstep(1.0 - distancePerPixel * aaSmoothing, 1.0, distanceToCenter);
                //float halfSmoothFactor = 0.5f * distancePerPixel * aaSmoothing;

                fillColor.a *= distanceAlphaFactor;

                return fillColor;
            }
            ENDCG
        }
    }
}
