Shader "Custom/FullscreenToggle2D"
{
    Properties
    {
        _Enabled ("Enabled", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Overlay"
        }

        Pass
        {
            Name "FullscreenPass"

            ZWrite Off
            ZTest Always
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float _Enabled;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                if (_Enabled < 0.5)
                    return half4(0, 0, 0, 0); // NO-OP

                // EFECTO DE EJEMPLO (pantalla rojiza)
                return half4(1, 0, 0, 0.3);
            }
            ENDHLSL
        }
    }
}
