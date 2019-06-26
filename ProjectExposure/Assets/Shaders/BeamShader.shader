Shader "Custom/BeamShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		[HDR]_TintColor ("Tint Color", Color) = (1,1,1,1)
		_Speed("Speed",float) = 9
		
		_Mitigation ("Mitigation",float) = 10
		
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		
		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				
				float2 noiseUV:TEXCOORD2;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _MainTex_ST;
			float4 _NoiseTex_ST;
			float4 _TintColor;
			float _Speed;
			float _Mitigation;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTex);
				
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				
				i.noiseUV.x += _Time.x * _Speed * -1;
				fixed4 noiseVal = tex2D(_NoiseTex, i.noiseUV);
				
				i.uv.x += noiseVal / _Mitigation;
				i.uv.y += sin(noiseVal / _Mitigation);
				
				
				fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
				// apply fog
				
				
				UNITY_APPLY_FOG(i.fogCoord, col);
				
				return col;
			}
			ENDCG
		}
	}
}
