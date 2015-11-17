Shader "Versatile/Unlit/Cutoff" {
	Properties
	 {
	 	_Color("color(RGB)",COLOR)=(1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Alpha ("Alpha Ramp", 2D) = "white" {}
		_Cutoff("Cutoff",Range(0,1))=0
	}
	SubShader 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True"}
		Blend SrcAlpha OneMinusSrcAlpha
		//Cull off
		//Zwrite off
		
				
	pass
	{		
		CGPROGRAM
		#pragma exclude_renderers d3d11 xbox360
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile LIGHTMAP_ON LIGHTMAP_OFF
		#include "unityCG.cginc"
		
		
		sampler2D _MainTex;
		sampler2D _Alpha;
		fixed _Cutoff;
		fixed4 _Color;
		
		

		struct v2f
		 {
			float4 pos:SV_POSITION;
			float2 uv:TEXCOORD0;
			//float2 UV1:TEXCOORD1;
			//float2 UV2:TEXCOORD2;
			//fixed4 color:Color;
		};

		v2f vert(appdata_full v)
		{
		  v2f o;
		  o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
		  float2 uv=v.texcoord.xy;
		  o.uv=uv;
		  return o;
		}
		
		fixed4 frag(v2f i):COLOR
		{
		 fixed4 t1=tex2D(_MainTex,i.uv)*_Color;
		 fixed4 alpha=tex2D(_Alpha,i.uv);
		 fixed4 final=t1;
		 fixed finalAlpha=alpha.a;
		 
		 if(finalAlpha<_Cutoff)
		 {
		  final.a=0;
		 }
		 
		 return final;
		}
		ENDCG
	  }//pass finish 
	}//subshader finish
	FallBack "Cutoffshader"
}//shader finish