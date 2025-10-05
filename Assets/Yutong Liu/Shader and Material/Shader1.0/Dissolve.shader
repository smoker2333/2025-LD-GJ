Shader "Custom/DissolveSphere"
{
    Properties
    {
         _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Dissolve ("Dissolve Amount", Range(0,1)) = 0.0
        _EdgeColor ("Edge Color", Color) = (1,0.5,0,1)
        _EdgeWidth ("Edge Width", Range(0.0,0.2)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        

        Pass
         {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            float _Dissolve;
            float4 _EdgeColor;
            float _EdgeWidth;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, IN.uv).r;

                // dissolve mask
                float dissolveValue = noise - _Dissolve;

                // edge
                float edge = smoothstep(0.0, _EdgeWidth, dissolveValue);

                if (dissolveValue < 0.0)
                {
                    // in dissolve zone
                    col.rgb = _EdgeColor.rgb;
                }

                col.a *= edge;
                return col;
            }
            ENDHLSL
        }
    }
}
