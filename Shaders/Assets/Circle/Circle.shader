Shader "Custom/Circle"
{
	Properties
	{
        _InnerRadius("Inner Radius", Float) = 0
        _InnerColor("Inner Color", Color) = (1, 0, 0, 0)
        _OuterRadius("Outer Radius", Float) = 0
        _OuterColor("Outer Color", Color) = (0, 0, 0, 0)
	}
	SubShader
	{
		Cull Back
        ZWrite On

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
            uniform float _InnerRadius;
            uniform float4 _InnerColor;
            uniform float _OuterRadius;
            uniform float4 _OuterColor;

			fixed4 frag (v2f i) : SV_Target
			{
                float2 offset = i.uv - float2(0.5, 0.5);
                float radius = length(offset);
                float ratio = smoothstep(_InnerRadius, _OuterRadius, radius);
                return lerp(_InnerColor, _OuterColor, ratio);
			}
			ENDCG
		}
	}
}
