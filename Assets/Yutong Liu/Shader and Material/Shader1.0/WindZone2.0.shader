Shader "Custom/WindZone2.0"
{
    Properties
    {
        _LinesTex("Lines Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture",2D) = "gray"{}
        _TintColor("Tint Color",Color) = (1,1,1,1)
        _WindSpeed("Wind Speed",Range(0,5)) = 1
        _NoiseScale("Noise Scale",Range(0.1,5)) = 1
        _LineBreakupStrength("Line Break Strength",Range(0,2)) = 0.5
        _WindDirection("Wind Direction(XY)",Vector) = (0,1,0,0)
        
        [Header(Distortion Settings)]
        _DistortionSpeed("Distortion Speed",Range(0,10)) = 2 // 扰动速度
        _DistortionStrength("Distortion Strength",Range(0,0.2)) = 0.02 // 扰动强度
        _DistortionFrequency("Distortion Frequency",Range(0.1,5)) = 1 // 扰动频率（噪声缩放）
        _DistortionDirection("Distortion Direction (XY)", Vector) = (1, 0, 0, 0) // 默认是左右扰动
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            TEXTURE2D(_LinesTex);
            SAMPLER(sampler_LinesTex);
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            float4 _LineTex_ST;
            float4 _NoiseTex_ST;
            float4 _TintColor;
            float4 _WindDirection;
            float _WindSpeed;
            float _NoiseScale;
            float _LineBreakupStrength;
            float _DistortionSpeed;
            float _DistortionStrength;
            float _DistortionFrequency;
            float4 _DistortionDirection;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                OUT.color = IN.color;
                return OUT;
            }

             half4 frag(Varyings IN):SV_Target
            {
                // 计算扰动量
                float2 distortionNoiseUV = IN.uv * _DistortionFrequency + float2(0, 1) * _DistortionSpeed * _Time.y;
                half distortion = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, distortionNoiseUV).r;
                float distortionOffset = (distortion - 0.5) * _DistortionStrength;

                // 线条和纹理根据风向和速度进行滚动
                float2 linesUV = IN.uv + _WindDirection.xy * _WindSpeed * _Time.y;
                
                linesUV += normalize(_DistortionDirection.xy) * distortionOffset;
                
                half4 linesColor = SAMPLE_TEXTURE2D(_LinesTex, sampler_LinesTex, linesUV);

                // 噪声纹理 (用于破碎线条，逻辑不变)
                float2 noiseUV = IN.uv * _NoiseScale + _WindDirection.xy * _WindSpeed * 0.7 * _Time.y;
                half noiseValue = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, noiseUV).r;

                // 用噪声来影响线条Alpha通道
                half finalAlpha = linesColor.r * (noiseValue * _LineBreakupStrength) * IN.color.a;

                // 最终颜色
                half4 finalColor = _TintColor;
                finalColor.a *= finalAlpha;

                return finalColor;
            }
            ENDHLSL
        }
    }
}
