Shader "Custom/Waves"
{
	Properties
	{
        _TopColor("Top Color", Color) = (0, 0, 0, 0)
        _BottomColor("Bottom Color", Color) = (0, 0, 0, 0)
        _Amplitude("Amplitude", Float) = 0
        _TimeFrequency("Time Frequency", Float) = 0
        _XFrequency("X Frequency", Float) = 0
        _ZFrequency("Z Frequency", Float) = 0
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
                float ratio : TEXCOORD1;
            };

            uniform float4 _TopColor;
            uniform float4 _BottomColor;
            uniform float _Amplitude;
            uniform float _TimeFrequency;
            uniform float _XFrequency;
            uniform float _ZFrequency;

			v2f vert (appdata v)
			{
                float4 position = v.vertex;
                float s = sin(_Time * _TimeFrequency + position.x * _XFrequency + position.z * _ZFrequency);
                position.y += s * _Amplitude;

				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, position);
				o.uv = v.uv;
                o.ratio = s * 0.5 + 0.5;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                return lerp(_BottomColor, _TopColor, i.ratio);
			}
			ENDCG
		}
	}
}
