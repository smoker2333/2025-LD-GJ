Shader "Custom/WindZone"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex("Noise Tex", 2D) = "gray"{}
        _WindSpeed("Wind Speed",Range(0,5)) = 1
        _WindStrength("Distortion Strength",Range(0,1)) = 0.05
        _WindDirection("Wind Direction",Vector) = (1,0,0,0)
        _TintColor("Tint Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
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

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float4 _TintColor;
            float4 _WindDirection;
            float _WindSpeed;
            float _WindStrength;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = TRANSFORM_TEX(IN.uv,_MainTex);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                //风的流动偏移
                float2 windFlow = _WindDirection.xy * _WindSpeed * _Time.y;

                //噪声扰动
                float2 noiseUV = IN.uv + windFlow;
                float noise = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex,noiseUV).r;

                //利用噪声扰动主纹理UV
                float2 distortedUV = IN.uv + (noise - 0.5) * _WindStrength;

                half4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,distortedUV) * _TintColor;
                return col;
            }
            ENDHLSL
        }
    }
}
