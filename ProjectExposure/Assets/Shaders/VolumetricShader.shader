Shader "Custom/Volumetric" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Pattern Texture", 2D) = "white" {}
		
		_Glossiness ("Smoothness", Range(0,1)) = 0
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_FresnelScale ("Fresnale Scale",Range(0,3)) = 1.85
		_FresnelColor ("Fresnel Color", color) = (1,1,1)
		[HDR] _Emission ("Emission Color", color) = (1,1,1)
		
		_WorldNormalScale ("World Normal Scale",Range(0,2)) = 1.2
		
		_NoiseSpeed("Noise Speed", Range(0.,1)) = 0.2
		
		_AlphaOffset("Alpha Offset", Range(0., 1.)) = 1.
		_Fade("Fade", Range(0., 10.)) = 1.
		
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		
		ZWrite off
        Blend SrcAlpha OneMinusSrcAlpha
       

		CGPROGRAM
		#pragma surface surf Standard noshadow alpha:fade decal:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		half3 _Emission;
		half3 _FresnelColor;

		struct Input {
    float2 uv_MainTex;
    float3 worldNormal;
    float3 viewDir;
    INTERNAL_DATA
};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _FresnelScale;
		float _WorldNormalScale;
		float _AlphaOffset;
		float _Fade;
		float _NoiseSpeed;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
		

		    float fresnel =  dot(IN.worldNormal * _WorldNormalScale, IN.viewDir);
		    fresnel = saturate (1 - fresnel);
		    fresnel = pow(fresnel, _FresnelScale);
		    
			// Albedo comes from a texture tinted by color
			IN.uv_MainTex.x += _Time.y * _NoiseSpeed;
			IN.uv_MainTex.y += _Time.y * _NoiseSpeed; 
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			float fade = saturate(pow(1. - IN.uv_MainTex.x, _Fade));
			
			o.Albedo = c.rgb ;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Color.a * fresnel * fade * _AlphaOffset;
			o.Emission = _Emission + _FresnelColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
