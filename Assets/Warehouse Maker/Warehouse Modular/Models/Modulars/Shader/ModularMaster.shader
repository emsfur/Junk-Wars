Shader "ModularMaster"
{
	Properties
	{
		[HideInInspector]_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_DoorColor("DoorColor", Color) = (1,1,1,0)
		_DoorTintInensity("DoorTintInensity", Range( 0 , 1)) = 0
		[HideInInspector]_TextureSample1("Texture Sample 1", 2D) = "white" {}
		[HideInInspector]_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_GarageDoorTintColor("GarageDoorTintColor", Color) = (1,1,1,0)
		_GarageDoorTintIntensity("GarageDoorTintIntensity", Range( 0 , 1)) = 0
		_OutsideWallColor01("OutsideWallColor01", Color) = (1,1,1,0)
		_OutsideWallTint01Intensity("OutsideWallTint01Intensity", Range( 0 , 1)) = 0
		_OutsideWallColor02("OutsideWallColor02", Color) = (1,1,1,0)
		_OutsideWallTint02Intensity("OutsideWallTint02Intensity", Range( 0 , 1)) = 0
		_InsideWallColor("InsideWallColor", Color) = (1,1,1,0)
		_InsideWallTintIntensity("InsideWallTintIntensity", Range( 0 , 1)) = 0
		[HideInInspector]_TextureSample3("Texture Sample 3", 2D) = "white" {}
		[HideInInspector]_TextureSample4("Texture Sample 4", 2D) = "white" {}
		[HideInInspector]_TextureSample5("Texture Sample 5", 2D) = "white" {}
		[HideInInspector]_TextureSample6("Texture Sample 6", 2D) = "white" {}
		[HideInInspector]_TextureSample7("Texture Sample 7", 2D) = "bump" {}
		[HideInInspector]_TextureSample8("Texture Sample 8", 2D) = "white" {}
		[HideInInspector]_TextureSample9("Texture Sample 9", 2D) = "white" {}
		[HideInInspector]_TextureSample10("Texture Sample 10", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample7;
		uniform float4 _TextureSample7_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float4 _DoorColor;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform float _DoorTintInensity;
		uniform float4 _GarageDoorTintColor;
		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;
		uniform float _GarageDoorTintIntensity;
		uniform float4 _OutsideWallColor01;
		uniform sampler2D _TextureSample3;
		uniform float4 _TextureSample3_ST;
		uniform float _OutsideWallTint01Intensity;
		uniform float4 _OutsideWallColor02;
		uniform sampler2D _TextureSample4;
		uniform float4 _TextureSample4_ST;
		uniform float _OutsideWallTint02Intensity;
		uniform float4 _InsideWallColor;
		uniform sampler2D _TextureSample5;
		uniform float4 _TextureSample5_ST;
		uniform float _InsideWallTintIntensity;
		uniform sampler2D _TextureSample6;
		uniform float4 _TextureSample6_ST;
		uniform sampler2D _TextureSample9;
		uniform float4 _TextureSample9_ST;
		uniform sampler2D _TextureSample10;
		uniform float4 _TextureSample10_ST;
		uniform sampler2D _TextureSample8;
		uniform float4 _TextureSample8_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample7 = i.uv_texcoord * _TextureSample7_ST.xy + _TextureSample7_ST.zw;
			o.Normal = UnpackNormal( tex2D( _TextureSample7, uv_TextureSample7 ) );
			float4 appendResult39 = (float4(i.vertexColor.r , i.vertexColor.g , i.vertexColor.b , 0.0));
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 lerpResult4 = lerp( tex2DNode1 , ( tex2DNode1 * _DoorColor ) , ( tex2D( _TextureSample1, uv_TextureSample1 ) * _DoorTintInensity ));
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float4 lerpResult13 = lerp( lerpResult4 , ( _GarageDoorTintColor * lerpResult4 ) , ( tex2D( _TextureSample2, uv_TextureSample2 ) * _GarageDoorTintIntensity ));
			float2 uv_TextureSample3 = i.uv_texcoord * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
			float4 lerpResult20 = lerp( lerpResult13 , ( _OutsideWallColor01 * lerpResult13 ) , ( tex2D( _TextureSample3, uv_TextureSample3 ) * _OutsideWallTint01Intensity ));
			float2 uv_TextureSample4 = i.uv_texcoord * _TextureSample4_ST.xy + _TextureSample4_ST.zw;
			float4 lerpResult28 = lerp( lerpResult20 , ( _OutsideWallColor02 * lerpResult20 ) , ( tex2D( _TextureSample4, uv_TextureSample4 ) * _OutsideWallTint02Intensity ));
			float2 uv_TextureSample5 = i.uv_texcoord * _TextureSample5_ST.xy + _TextureSample5_ST.zw;
			float4 lerpResult34 = lerp( lerpResult28 , ( _InsideWallColor * lerpResult28 ) , ( tex2D( _TextureSample5, uv_TextureSample5 ) * _InsideWallTintIntensity ));
			float2 uv_TextureSample6 = i.uv_texcoord * _TextureSample6_ST.xy + _TextureSample6_ST.zw;
			float4 lerpResult43 = lerp( ( appendResult39 * lerpResult34 ) , lerpResult34 , ( 1.0 - tex2D( _TextureSample6, uv_TextureSample6 ) ));
			o.Albedo = lerpResult43.rgb;
			float2 uv_TextureSample9 = i.uv_texcoord * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
			o.Metallic = tex2D( _TextureSample9, uv_TextureSample9 ).r;
			float2 uv_TextureSample10 = i.uv_texcoord * _TextureSample10_ST.xy + _TextureSample10_ST.zw;
			o.Smoothness = tex2D( _TextureSample10, uv_TextureSample10 ).r;
			float2 uv_TextureSample8 = i.uv_texcoord * _TextureSample8_ST.xy + _TextureSample8_ST.zw;
			o.Occlusion = tex2D( _TextureSample8, uv_TextureSample8 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
