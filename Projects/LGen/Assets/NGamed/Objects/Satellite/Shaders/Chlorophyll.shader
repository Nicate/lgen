// This is a replacement shader.
Shader "lgen/Chlorophyll" {
	SubShader {
		Tags {
			"Responsive"="Yes"
		}
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"


				float4 vert(float4 vertex : POSITION) : SV_POSITION {
					return UnityObjectToClipPos(vertex);
				}

				float4 frag() : SV_Target {
					// Green.
					return float4(0.0, 1.0, 0.0, 1.0);
				}
			ENDCG
		}
	}
	SubShader {
		Tags {
			"Responsive"="No"
		}
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"


				float4 vert(float4 vertex : POSITION) : SV_POSITION {
					return UnityObjectToClipPos(vertex);
				}

				float4 frag() : SV_Target {
					// Black.
					return float4(0.0, 0.0, 0.0, 1.0);
				}
			ENDCG
		}
	}
}
