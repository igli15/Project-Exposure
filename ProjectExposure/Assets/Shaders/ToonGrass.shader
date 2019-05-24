Shader "Custom/NewToon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		_WindTex ("Wind Texutre",2D)= "white"{}
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        [HDR]
       _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
       _Glossiness("Glossiness", Float) = 32

       [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716

        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        
        _Amplitude ("Amplitude", Float) = 1
		_WaveSpeed ("Waving Speed", vector) = (1,1,1,1)
		_WindSpeed ("Wind Speed",vector) = (1,1,0,0)
		_WorldSize("World Size", vector) = (1, 1, 1, 1)
		_AxisEffected("Axis Which Will Move",vector) = (1,0,1)

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
                SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

            float _Glossiness;
            float4 _SpecularColor;

            float4 _RimColor;
            float _RimAmount;

            float _RimThreshold;
            
           
        float _Amplitude;
		float _Wavelength;
		float3 _WaveSpeed;
		float4 _WorldSize;
		float3 _AxisEffected;
		float2 _WindSpeed;
		sampler2D _WindTex;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o)
                o.viewDir = WorldSpaceViewDir(v.vertex);
                      
                float4 worldPos = mul(v.vertex, unity_ObjectToWorld);
               
                float2 samplePos = worldPos.xz/_WorldSize.xz;

                samplePos += _Time.y * _WindSpeed.xy;

                float windSample = tex2Dlod(_WindTex, float4(samplePos, 0, 0));
            
                o.pos.x += cos(_WaveSpeed.x*windSample)*_Amplitude * _AxisEffected.x;
                o.pos.y += sin(_WaveSpeed.y*windSample)*_Amplitude * _AxisEffected.y;
                o.pos.z += sin(_WaveSpeed.z*windSample)*_Amplitude * _AxisEffected.z;
			
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


				float4 sample = tex2D(_MainTex, i.uv);

              return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}