Shader "Custom/Transparence"
{
	Properties
	{
        _MainTex("Diffuse Texture", 2D) = "white" {}
        _Cloud("Cloud Texture", 2D) = "white" {}
        _AlphaOffset("Alpha Offset", Float) = 0
	}
	SubShader
	{
		Cull Back
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
        
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

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
			
            uniform sampler2D _MainTex;
            uniform sampler2D _Cloud;
            uniform float _AlphaOffset;

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 color = tex2D(_MainTex, i.uv);
                color.a = tex2D(_Cloud, i.uv).r + _AlphaOffset;
				return color;
			}
			ENDCG
		}
	}
}
