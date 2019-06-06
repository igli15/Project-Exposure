// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Particles/FadingShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Gradient (" Gradient", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader 
	{
	    Tags { "Queue"="Transparent" "RenderType"="Transparent" }
 
 Pass
 {
        Cull Back
        ZWrite Off
        Blend srcAlpha OneMinusSrcAlpha
        
        
		CGPROGRAM
	
		#pragma vertex vert
		#pragma fragment frag
		  

		#pragma fragmentoption ARB_precision_hint_fastest
 
        #include "UnityCG.cginc"
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _Gradient;
		float4 _Gradient_ST;
	    fixed4 _Color;
	     

		struct appdata {
                half4 vertex : POSITION;
                half2 texcoord : TEXCOORD0;
                half2 gradientUV:TEXCOORD1;
                fixed4 color : COLOR;
            };
           
            //VertIn
            struct v2f {
                half4 pos : POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
                half2 gradientUV:TEXCOORD1;
            };



		v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.color = v.color;
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.gradientUV = TRANSFORM_TEX(v.gradientUV, _Gradient);
                return o;
            }
           
 
            fixed4 frag (v2f i) : COLOR
            {
                fixed4 col;
                
                i.gradientUV.y += _Time.y;
                fixed4 tex = tex2D(_MainTex, i.texcoord);
                fixed4 g = tex2D(_Gradient,i.gradientUV);
                col.rgb = i.color.rgb * tex.rgb;
                col.a = i.color.a * tex.a * g.r;
                return col;
               
            }
		ENDCG
		}
	}
	
	FallBack "Diffuse"
}
