Shader "Custom/UnderWaterPostEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScrollX ("DistortionNoise", Range(-150,150)) = 150
		_ScrollY("DistortionFrequency",Range(0,5)) = 1
        _Amount("Wave Amount", Range(0,1)) = 0.5

	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _ScrollX;
			float _ScrollY;

			fixed4 frag (v2f IN) : SV_Target
			{
				//DistortionEffect
				fixed4 col = tex2D(_MainTex, IN.uv + float2(0,sin( IN.vertex.x/50+_Time[_ScrollY] ) )/_ScrollX );
				col.b =0.2f;
				return col;
			}
			ENDCG
		}
	}
}
