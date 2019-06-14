Shader "Custom/RainbowToon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		_EmissionTex("Emission Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture",2D) = "white"{}
		_Mitigation("Mitigation", Range(1, 20)) = 5
 	
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
				float4 noiseUV :TEXCOORD1;
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 noiseUV :TEXCOORD1;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
                SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			sampler2D _EmissionTex;
			sampler2D _NoiseTex;
			float4 _MainTex_ST;
			float4 _NoiseTex_ST;
			float4 _EmissionColor;
			
            float _EmissionScale;
            float _Glossiness;
            float4 _SpecularColor;

            float4 _RimColor;
            float _RimAmount;
            float _Mitigation;

            float _RimThreshold;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.noiseUV = TRANSFORM_TEX(v.noiseUV, _NoiseTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o)
               UNITY_TRANSFER_FOG(o,o.pos);
               
                o.viewDir = WorldSpaceViewDir(v.vertex);
                 
				return o;
			}
			
			float4 _Color;
            float4 _AmbientColor;

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
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

            i.noiseUV.x += _Time.x;
            float noise = tex2D(_NoiseTex,i.noiseUV);
            
            i.uv.x += noise/_Mitigation + _Time.x;
            
            
			float4 sample = tex2D(_MainTex, i.uv);
				
			float4 emission = tex2D(_EmissionTex, i.uv) * _EmissionColor;
			emission *= _EmissionScale;
			
			
             float4 result = _Color * sample * (_AmbientColor + light + specular + rim + emission);
             UNITY_APPLY_FOG(i.fogCoord, result);
             return result;
			}
			ENDCG
		}
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}