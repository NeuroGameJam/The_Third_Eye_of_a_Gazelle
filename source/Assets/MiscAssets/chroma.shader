Shader "Chroma/Multicolor" {
 Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _TextureA ("Base (RGB)", 2D) = "white" {}
 _ColorA ("Colour", Color ) = (1,1,1,1)
 _TextureB ("Base (RGB)", 2D) = "white" {}
  _ColorB ("Colour", Color ) = (1,1,1,1)
 _TextureC ("Base (RGB)", 2D) = "white" {}
 _ColorC ("Colour", Color) = (1,1,1,1)
 _ColorLineIn ("Colour", Color) = (1,1,1,1)
 _ColorLineOut ("Colour", Color) = (1,1,1,1)
 _Invert ("Invert", Float) = 0

 }
 SubShader {
 Pass {
 CGPROGRAM
 #pragma vertex vert_img
 #pragma fragment frag
 
 #include "UnityCG.cginc"
 
 uniform sampler2D _MainTex;
 uniform sampler2D _TextureA;
 uniform sampler2D _TextureB;
 uniform sampler2D _TextureC;
 uniform float4 _ColorA;
 uniform float4 _ColorB;
 uniform float4 _ColorC;
 uniform float4 _ColorLineIn;
 uniform float4 _ColorLineOut;
 uniform float _Invert;
 
 float4 frag(v2f_img i) : COLOR {
 float4 c = tex2D(_MainTex, i.uv);

if (abs(c.r - _ColorA.r) < 0.01)// && c.g == _ColorA.g && c.b == _ColorA.b)
	c = tex2D(_TextureA, i.uv);
else if (abs(c.r - _ColorB.r) < 0.2 && abs(c.g - _ColorB.g) < 0.2 && abs(c.b - _ColorB.b) < 0.2)
	c = tex2D(_TextureB, i.uv);
else if (abs(c.r - _ColorC.r) < 0.2 && abs(c.g - _ColorC.g) < 0.2 && abs(c.b - _ColorC.b) < 0.2)
	c = tex2D(_TextureC, i.uv);

if (abs(c.r - _ColorLineIn.r) < 0.1 && abs(c.g - _ColorLineIn.g) < 0.1 && abs(c.b - _ColorLineIn.b) < 0.1)
	c = _ColorLineOut;

	//c = float4(1-c.r, 1-c.g, 1-c.b,1);
	//c = float4(c.g, c.b, c.r,1);
	//c = float4(c.b, c.r, c.g,1);
	c = float4(c.b, c.g, c.r,1);


//float t = (_Invert*3);
//if (_Invert != 0){
//	if (!(abs(c.r - 1) < t && abs(c.g - 1) < t && abs(c.b - 1) < t))
//		c = lerp(float4(1,1,1,1)*0, c, t*10);
		//c = float4(1,1,0,1);
		//c = float4(1-c.r, 1-c.g, 1-c.b,1);
//}

 	return c;
 }

 ENDCG
 }
 }
}