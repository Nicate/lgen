Shader "lgen/Responsive" {
    Properties {
		[NoScaleOffset] AlbedoTexture("Albedo", 2D) = "white" {}
		[NoScaleOffset] [Normal] NormalTexture("Normal", 2D) = "bump" {}
		[NoScaleOffset] SmoothnessTexture("Smoothness", 2D) = "black" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Responsive"="Yes" }
        LOD 200

        CGPROGRAM
			#pragma surface surf Standard

			#pragma target 3.0


			struct Input {
				float2 uvAlbedoTexture;
			};


			sampler2D AlbedoTexture;
			sampler2D NormalTexture;
			sampler2D SmoothnessTexture;
			
			
			void surf(Input input, inout SurfaceOutputStandard output) {
				float2 coordinates = input.uvAlbedoTexture;

				output.Albedo = tex2D(AlbedoTexture, coordinates);
				output.Normal = UnpackNormal(tex2D(NormalTexture, coordinates));
				output.Smoothness = tex2D(SmoothnessTexture, coordinates).a;
				output.Metallic = 0.0;
			}
        ENDCG
    }
    FallBack "Diffuse"
}