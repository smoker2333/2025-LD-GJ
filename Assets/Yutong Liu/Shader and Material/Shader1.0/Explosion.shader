Shader "Custom/Explosion"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture",2D) = "gray"{}
        _EdgeColor("Edge Color",Color) = (1,0.6,0.2,1)
        _EdgeWidth("Edge Width",Range(0,0.3)) = 0.1
        _DissolveSpeed("Dissolve Speed",Range(0,3)) = 1
        _Distortion("Distortion Strength",Range(0,0.05)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _EdgeColor;
            float _EdgeWidth;
            float _DissolveSpeed;
            float _Distortion;

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

            float _TimeValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               //时间参数
                _TimeValue = _Time.y * _DissolveSpeed;

                //uv扭曲，噪声随时间波动
                float2 noiseUV = i.uv + float2(sin(_TimeValue),cos(_TimeValue)) * _Distortion;
                float noise = tex2D(_NoiseTex,noiseUV).r;

                //利用时间推进溶解阈值
                float threshold = saturate(_TimeValue);

                //主帖图采样
                fixed4 col = tex2D(_MainTex,i.uv);

                //噪声与阈值的判断关系，控制溶解
                float dissolve = step(threshold,noise);

                //边缘范围
                float edge = smoothstep(threshold,threshold + _EdgeWidth,noise);

                //让边缘部分亮起来
                col.rgb = lerp(col.rgb,_EdgeColor.rgb,edge);

                //应用溶解
                col.a *= dissolve;

                return col;
            }
            ENDCG
        }
    }
}
