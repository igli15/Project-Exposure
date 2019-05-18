Shader "Handout/Fish" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Emission ("Emission (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_Speed ("Speed", Range(0,2)) = 1
		
		_Amplitude ("Amplitude",Range(0,1)) = 0.2
		_Yaw ("Yaw" ,Range(-2,2)) = 0.5
		_Roll ("Roll" ,Range(-2,2)) = 0.5
		_Limit ("Movement Limit",Range (-2,2)) = 0
		
	}
	SubShader {
		Tags {  "RenderType"="Transparent" "Queue"="Transparent"  }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard vertex:vert addshadow
		#pragma target 3.0

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
		float _Speed;
		
		float _Yaw;
		float _Roll;
		float _Limit;

		void vert(inout appdata_full vertexData) 
		{
		    float3 p = vertexData.vertex.xyz;
		    
		    float l = p.z < _Limit;
		    p += ((sin((( _Time.w * _Speed) + (p.z * _Yaw) + ( p.y * _Roll ))) * _Amplitude) * float3(1,0,0)) * l;    

			vertexData.vertex.xyz = p;
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

