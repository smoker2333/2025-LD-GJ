Shader "Custom/ConveyorBelt"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Speed("Scroll Speed",Float) = 1.0
        _Direction("Scroll Direction",Vector) = (1,0,0,0)
        _Brightness("Brightness",Range(0,3)) = 1.0
        _GlowColor("Glow Color",Color) = (1,1,1,1)
        _GlowWidth("Glow Width",Range(0.01,0.5)) = 0.2
        _GlowSpeed("Glow Speed",Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        
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
            float4 _MainTex_ST;

            float _Speed;
            float4 _Direction;
            float _Brightness;
            float4 _GlowColor;
            float _GlowWidth;
            float _GlowSpeed;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float2 offset = _Direction.xy * _Speed * _Time.y;
                float2 uv = frac(IN.uv + offset);

                half4 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv) * _Brightness;

                // 扫描光带
                float glowLine = smoothstep(0.5 - _GlowWidth, 0.5, frac(uv.y + _Time.y * _GlowSpeed));
                half4 glow = _GlowColor * glowLine;

                return baseColor + glow * 0.5;
            }
            ENDHLSL
        }
    }
}
