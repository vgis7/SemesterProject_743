Shader "Custom/PointShader"{
	Properties{
		_Color("Main Color", Color) = (1,0,0,1)
	}
	SubShader{
		Tags { "RenderType" = "Point" }
			Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 vert(float4 vertex : POSITION) : SV_POSITION{
				return UnityObjectToClipPos(vertex);
			}

			fixed4 _Color;

			fixed4 frag() : SV_Target{
				return _Color;
			}
			ENDCG
		}
	}
}
