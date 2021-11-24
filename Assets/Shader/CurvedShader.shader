// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Curved"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BendAmountX("Bend Ammount X", Float) = 0.0
		_BendAmountY("Bend Ammount Y", Float) = 0.0
		_BendFalloff("Bend FallOff", Float) = 0.0
		_BendOrigin("Bend Origin", Vector) = (0,0,0,0)

	}

		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		// Global Shader values
		
		float4 _BendOrigin;
		float _BendFalloff;
		float _BendAmountX;
		float _BendAmountY;

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			  float2 uv_MainTex;
		};

		float4 Curve(float4 v)
		{
			_BendAmountX *= 0.0001;
			_BendAmountY *= 0.0001;

			float4 world = mul(unity_ObjectToWorld, v);

			float dist = length(world.xz - _BendOrigin.xz);

			dist = max(0, dist - _BendFalloff);

			// Distance squared
			dist = dist * dist;

			world.x += dist * _BendAmountX;
			world.y += dist * _BendAmountY;
			return mul(unity_WorldToObject, world);
	  }

	  void vert(inout appdata_full v)
	  {
			v.vertex = Curve(v.vertex);
	  }

	  void surf(Input IN, inout SurfaceOutput o)
	  {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
	  }

	  ENDCG
	}

		Fallback "Mobile/Diffuse"
}
