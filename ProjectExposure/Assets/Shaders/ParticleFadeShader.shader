// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Particles/FadingShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Gradient (" Gradient", 2D) = "white" {}
		
		 _Speed ("Speed", Range(0,2)) = 0.5
		 
		[Toggle(MOVE GRADIENT VERICALLY)]
        _MoveVertically ("Move Gradient Vertically", Float) = 1
        
        [Toggle(MOVE GRADIENT Horizontally)]
        _MoveHorizontally ("Move Gradient Horizontally", Float) = 0
		
		[Toggle(USE NOISE)]
        _UseNoise ("Use Noise", Float) = 0
        _NoiseTex ("Noise Tex", 2D) = "white" {}
        _Mitigation ("Mitigation", Range(0,20)) = 10
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
		sampler2D _NoiseTex;
		float4 _Gradient_ST;
	    fixed4 _Color;
	    float _MinAlpha;
	    float _UseNoise;
	    float _Mitigation;
	     float _Speed;
	     float _MoveHorizontally;
	     float _MoveVertically;

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
                //i.gradientUV.x = (cos(_Time.y) +1)/2;
               // i.gradientUV.y = (sin(_Time.y) + 1)/2;
                fixed4 noiseVal = tex2D(_NoiseTex,i.texcoord);
                
                i.gradientUV.x += cos(noiseVal / _Mitigation);
				i.gradientUV.y += sin(noiseVal / _Mitigation);
                 
                if(_MoveVertically)
                i.gradientUV.y += _Time.y *_Speed;
                
                if(_MoveHorizontally)
                i.gradientUV.x += _Time.y * _Speed;  

                fixed4 tex = tex2D(_MainTex, i.texcoord);
               
                
                fixed4 g = tex2D(_Gradient,i.gradientUV);
                col.rgb = i.color.rgb * tex.rgb;
                
                float finalAlpha = i.color.a * tex.a * g.r;
                
                col.a = finalAlpha;
                
                return col;
               
            }
		ENDCG
		}
	}
	
	FallBack "Diffuse"
}
