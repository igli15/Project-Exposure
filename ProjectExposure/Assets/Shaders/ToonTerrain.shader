﻿

Shader "Custom/ToonWithNormals"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		_NormalMap("NormalMap", 2D) = "white" {}
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        [HDR]
       _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
       _Glossiness("Glossiness", Float) = 32

       [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716

        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        
        [HDR] _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0,5)) = 0

	}
	SubShader
	{
		Pass
		{
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }

			CGPROGRAM
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
                float3 tangent : TANGENT;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                float3 T : TEXCOORD3;
				float3 B : TEXCOORD4;
				float3 N : TEXCOORD5;
				UNITY_FOG_COORDS(6)
                SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			sampler2D _NormalMap;
			float4 _MainTex_ST;

            float _Glossiness;
            float4 _SpecularColor;

            float4 _RimColor;
            float _RimAmount;

            float _RimThreshold;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o)
                UNITY_TRANSFER_FOG(o,o.pos);

                o.viewDir = WorldSpaceViewDir(v.vertex);
                
                float3 worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
				float3 worldTangent = mul((float3x3)unity_ObjectToWorld, v.tangent);
				
				float3 binormal = cross(v.normal, v.tangent.xyz);
				float3 worldBinormal = mul((float3x3)unity_ObjectToWorld, binormal);

				o.N = normalize(worldNormal);
				o.T = normalize(worldTangent);
				o.B = normalize(worldBinormal);
				
				return o;
			}
			
			float4 _Color;
            float4 _AmbientColor;

			float4 frag (v2f i) : SV_Target
			{
			    
			    float3 tangentNormal = tex2D(_NormalMap, i.uv).xyz;
			
				tangentNormal = normalize(tangentNormal * 2 - 1);
			
				float3x3 TBN = float3x3(normalize(i.T), normalize(i.B), normalize(i.N));
				TBN = transpose(TBN);

				float3 worldNormal = mul(TBN, tangentNormal);
				
                float3 normal = normalize(worldNormal);
                
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
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                
                
				float4 sample = tex2D(_MainTex, i.uv);

               float4 result = _Color * sample * (_AmbientColor + light + specular + rim);
               UNITY_APPLY_FOG(i.fogCoord, result);
              return result;
			}
			ENDCG
		}
		
		Pass
		{
		    Cull front
		        
		    CGPROGRAM
		    
		   
            #include "UnityCG.cginc"

          
            #pragma vertex vert
            #pragma fragment frag

          
            fixed4 _OutlineColor;
            
            float _OutlineThickness;
            
           
            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            
            struct v2f{
                float4 position : SV_POSITION;
            };


            v2f vert(appdata v){
                v2f o;

                float3 normal = normalize(v.normal);
                float3 outlineOffset = normal * _OutlineThickness;
                float3 position = v.vertex + outlineOffset;

                o.position = UnityObjectToClipPos(position);

                return o;
            }

            
            fixed4 frag(v2f i) : SV_TARGET{
                return _OutlineColor;
            }
		    
		    ENDCG
		        
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}