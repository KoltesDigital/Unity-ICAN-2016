Shader "Custom/Bar"
{
    Properties
    {
        _MainTex("Diffuse Texture", 2D) = "white" {}
        _Ratio("Ratio", Float) = 1
        _UMin("U Min", Float) = 0
        _UMax("U Max", Float) = 1
        _Bevel("Bevel", Float) = 1
    }
    
    SubShader
    {
        Cull Back
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        Pass
        {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            uniform sampler2D _MainTex;
            uniform float _Ratio;
            uniform float _UMin;
            uniform float _UMax;
            uniform float _Bevel;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                float u = lerp(_UMin, _UMax, _Ratio) - (1.0 - i.uv.y) * _Bevel;
                color.a *= 1.0 - step(u, i.uv.x);
                return color;
            }

            ENDCG
        }
    }
}