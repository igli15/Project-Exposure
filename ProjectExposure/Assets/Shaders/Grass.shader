
Shader "Handout/Grass" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_WindTex ("Wind Texutre",2D)= "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amplitude ("Amplitude", Float) = 1
		_WaveSpeed ("Waving Speed", vector) = (1,1,1,1)
		_WindSpeed ("Wind Speed",vector) = (1,1,0,0)
		_WorldSize("World Size", vector) = (1, 1, 1, 1)
		_AxisEffected("Axis Which Will Move",vector) = (1,0,1)
	}
	SubShader {
		Tags {  "DisableBatching" = "True"  }
              

		CGPROGRAM
		#pragma surface surf Standard vertex:vert 
		#pragma target 3.0
		#include "UnityCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		half _Emission;
		fixed4 _Color;
		float _Amplitude;
		float _Wavelength;
		float3 _WaveSpeed;
		float4 _WorldSize;
		float3 _AxisEffected;
		float2 _WindSpeed;
		sampler2D _WindTex;

		void vert(inout appdata_base vertexData) 
		{
            float3 output = vertexData.vertex.xyz;
            
            float4 worldPos = mul(vertexData.vertex, unity_ObjectToWorld);
               
            float2 samplePos = worldPos.xz/_WorldSize.xz;

            samplePos += _Time.y * _WindSpeed.xy;

            float windSample = tex2Dlod(_WindTex, float4(samplePos, 0, 0));
            
            output.x += cos(_WaveSpeed.x*windSample)*_Amplitude * _AxisEffected.x;
            output.y += sin(_WaveSpeed.y*windSample)*_Amplitude * _AxisEffected.y;
            output.z += sin(_WaveSpeed.z*windSample)*_Amplitude * _AxisEffected.z;
            
			vertexData.vertex.xyz = output;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission = _Emission;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

