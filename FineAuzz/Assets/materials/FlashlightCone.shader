Shader "Custom/FlashlightCone"
{
    Properties
    {
        _CoreColor ("Core Color", Color) = (1, 0.95, 0.8, 1)
        _EdgeColor ("Edge Color", Color) = (0.6, 0.7, 1, 1)
        _Intensity ("Intensity", Float) = 2
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend One One
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _CoreColor;
            float4 _EdgeColor;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float edge = abs(i.uv.x - 0.5);
                float edgeFade = smoothstep(0.35, 0.5, edge); // <--- fixed

                float distFade = smoothstep(1.0, 0.0, i.uv.y);
                    
                float alpha = edgeFade * distFade;

                float3 color = lerp(_EdgeColor.rgb, _CoreColor.rgb, distFade);
                return float4(color * alpha * _Intensity, alpha);
            }
            ENDHLSL
        }
    }
}