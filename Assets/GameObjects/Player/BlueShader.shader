Shader "Custom/BlueShader" {
		Properties{
			_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_ColorTint("Tint", Color) = (0.0, 0.0, 1.0, 1.0)
		}
			SubShader{
			Tags{ "RenderType" = "Opaque" }
			CGPROGRAM
#pragma surface surf Lambert finalcolor:bluecolor
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		fixed4 _ColorTint;
		void bluecolor(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color *= _ColorTint;
		}

		sampler2D _MainTex;
		sampler2D _BumpMap;
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
		}
			Fallback "Diffuse"
	}
