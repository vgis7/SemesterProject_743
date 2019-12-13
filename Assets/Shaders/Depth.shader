// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Depth"{
    SubShader{
        Tags { "RenderType"="Point" }
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata {
				float4 vertex : POSITION;
			};
			
			struct v2f {
				float4 vertex : SV_POSITION;
				float4 depth : DEPTH;
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // Equal to: mul(UNITY_MATRIX_MVP, v.vertex);
				o.depth = -UnityObjectToViewPos(v.vertex).z*_ProjectionParams.w; //Eual to: o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z*_ProjectionParams.w;
				return o;
			}
				
			fixed4 frag(v2f i) : SV_Target{
				float invertDepth = 1 - i.depth;
				fixed4 colorRed = fixed4(1, 0, 0, 1);
				fixed4 colorGreen = fixed4(0, 1, 0, 1);
				fixed4 colorBlue = fixed4(0, 0, 1, 1);
				float middle = 0.6;
				fixed4 mixedColor = lerp(colorBlue, colorGreen, middle/invertDepth)*step(invertDepth, middle);
				mixedColor += lerp(colorGreen, colorRed, (invertDepth - middle) / (1 - middle))*step(middle, invertDepth);
				mixedColor.a = 1;

				return mixedColor;
			}
			ENDCG
		}
    }
    FallBack "Diffuse"
}
