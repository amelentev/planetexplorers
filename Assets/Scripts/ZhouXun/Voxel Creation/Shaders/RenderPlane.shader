// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Voxel Creation/RenderPlane"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Tile ("Tile", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Transparent+10000" }
		Pass
		{
			Fog { Mode Off } 
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			//ZWrite Off 
			
			CGPROGRAM 
			#pragma exclude_renderers gles
			#pragma vertex vert
			#pragma fragment frag
	
			sampler2D _MainTex; 
			float _Tile;
	
			struct appdata_v
			{
				float4	vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			struct v2f 
			{
				float3 texc : TEXCOORD0;
				float4 pos : POSITION0;
			};
			
			v2f vert (appdata_v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos( v.vertex);				
				o.texc = v.texcoord;	
				return o;
			}
			half4 frag (v2f i) : COLOR
			{
				half4 c = tex2D (_MainTex, frac(-i.texc*_Tile));
				return c;
			}
			ENDCG 
		}
	} 
}
