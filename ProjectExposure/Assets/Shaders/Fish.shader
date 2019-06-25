Shader "Custom/Fish" {
	Properties {
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		_EmissionTex("Emission Texture", 2D) = "white" {}	
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        [HDR]
       _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
       _Glossiness("Glossiness", Float) = 32

       [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716

        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        
         _EmissionScale("Emission Scale", Range(0, 5)) = 0
         [HDR]
        _EmissionColor("Emission Color", Color) = (1,1,1,1)
		
		_Speed ("Speed", Range(0,2)) = 1
		
		_Amplitude ("Amplitude",Range(0,1)) = 0.2
		_Yaw ("Yaw" ,Range(-2,2)) = 0.5
		_Roll ("Roll" ,Range(-2,2)) = 0.5
		_Limit ("Movement Limit",Range (-4,4)) = 0
		
	}
	SubShader {
		

		Pass
		{
		Tags {  "RenderType"="Transparent" "Queue"="Transparent"  }
		LOD 200
		CGPROGRAM
		#pragma target 3.0
		#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fwdbase
             #pragma multi_compile_fog
			
			#include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

	

		struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                UNITY_FOG_COORDS(4)
                SHADOW_COORDS(2)
			};

    		sampler2D _MainTex;
			sampler2D _EmissionTex;
			float4 _MainTex_ST;
			float4 _EmissionColor;
			
            float _EmissionScale;
            float _Glossiness;
            float4 _SpecularColor;
            float4 _Color;
            float4 _AmbientColor;

            float4 _RimColor;
            float _RimAmount;

            float _RimThreshold;
		
		float _Wavelength;
		float _Speed;
		
		float _Yaw;
		float _Roll;
		float _Limit;
		float _Amplitude;

		v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				float3 p = o.pos;
		    
		        float l = p.z < _Limit;
		        p += ((sin((( _Time.w * _Speed) + (p.z * _Yaw) + ( p.y * _Roll ))) * _Amplitude) * float3(1,0,0)) * l;    
		    
		        o.pos.xyz = p;
		        
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o)
               UNITY_TRANSFER_FOG(o,o.pos);
               
                o.viewDir = WorldSpaceViewDir(v.vertex);
                 
				return o;
			}

		float4 frag (v2f i) : SV_Target
			{
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 light = lightIntensity * _LightColor0;

                float3 viewDir = normalize(i.viewDir);

                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;

                float4 rimDot = 1 - dot(viewDir, normal);
              float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.05, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;


			float4 sample = tex2D(_MainTex, i.uv);
				
			float4 emission = tex2D(_EmissionTex, i.uv) * _EmissionColor;
			emission *= _EmissionScale;
			
			
             float4 result = _Color * sample * (_AmbientColor + light + specular + rim + emission);
             UNITY_APPLY_FOG(i.fogCoord, result);
             return result;
			}
			ENDCG
			}
		
	}
	FallBack "Diffuse"
}

