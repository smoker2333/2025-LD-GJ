Shader "Custom/LaserBeam"
{
    Properties
    {
        _MainTex ("Main Texture (Linear Gradient or Noise)", 2D) = "white" {}
        _Color ("Laser Color", Color) = (1,0,0,1)
        _Strength ("Laser Strength", Range(0, 10)) = 3.0 
        _CoreWidth ("Core Width", Range(0.01, 0.5)) = 0.1
        _EdgeGlow ("Edge Glow", Range(0.01, 1.0)) = 0.5
        _ScrollSpeed ("Scroll Speed (X, Y)", Vector) = (0.5, 0.0, 0.0, 0.0)
        _PulseFrequency ("Pulse Frequency", Range(0, 10)) = 2.0
        _PulseMagnitude ("Pulse Magnitude", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

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
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            float4 _MainTex_ST;
            float4 _Color;
            float _Strength;
            float _CoreWidth;
            float _EdgeGlow;
            float4 _ScrollSpeed;
            float _PulseFrequency;
            float _PulseMagnitude;
            
            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex); 
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                //纹理滚动动画
                float2 scrolledUV = IN.uv + _ScrollSpeed.xy * _Time.y;
                half texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, scrolledUV).r;

                //脉冲动画
                float pulse = 1.0 - (sin(_Time.y * _PulseFrequency) * 0.5 + 0.5) * _PulseMagnitude;

                //激光形状和发光计算
                float distFromCenter = abs(IN.uv.y - 0.5) * 2.0; 

                half core = smoothstep(pulse * _CoreWidth, 0.0, distFromCenter);
                half glow = smoothstep(pulse * (_CoreWidth + _EdgeGlow), _CoreWidth * pulse, distFromCenter);

                //组合颜色和Alpha
                half3 finalColor = _Color.rgb * texColor;
                half finalAlpha = saturate(core + glow) * _Color.a;

                //应用强度
                finalColor *= (core * _Strength) + (glow * _Strength * 0.5);

                return half4(finalColor, finalAlpha);
            }
            ENDHLSL
        }
    }
}
