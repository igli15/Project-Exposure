﻿Shader "Custom/Frequency" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Amplitude ("Amplitude", Float) = 1
		_Wavelength ("Wavelength", Float) = 10
		_Speed ("Speed", Float) = 1
	}
	SubShader {
		Tags {  "RenderType"="Transparent" "Queue"="Transparent"  }
		LOD 200

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
		CGPROGRAM
		#pragma surface surf Standard alpha:blend vertex:vert noshadow
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		float _Amplitude;
		float _Wavelength;
		float _Speed;

		void vert(inout appdata_full vertexData) 
		{
		    float3 p = vertexData.vertex.xyz;

            float k = 2 * UNITY_PI / _Wavelength;
            float f = k * (p.z - _Speed * _Time.y);
			p.y += _Amplitude * sin(f);
			
			float3 tangent = normalize(float3(1, k * _Amplitude * cos(f), 0));
            float3 normal = float3(-tangent.y, tangent.x, 0);
            
            
			vertexData.vertex.xyz = p;
			//vertexData.normal = normal;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			o.Albedo = c.rgb;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

