Shader "Custom/AOEShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Pattern Texture", 2D) = "white" {}
		
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_FresnelScale ("Fresnale Scale",Range(0,3)) = 1
		_FresnelColor ("Fresnel Color", color) = (1,1,1)
		[HDR] _Emission ("Emission Color", color) = (1,1,1)
		
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		
		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:fade

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

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
		
		    
		    float fresnel =  dot(IN.worldNormal, IN.viewDir);
		    fresnel = saturate (1 - fresnel);
		    fresnel = pow(fresnel, _FresnelScale);
		    
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * fresnel;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Color.a * fresnel;
			o.Emission = _Emission + _FresnelColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
