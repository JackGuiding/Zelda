Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // 顶点着色器 Vertex Shader
            // 处理顶点
            // 逐顶点处理
            // 最终影响形状
            v2f vert (appdata v)
            {
                // if (v.vertex.x < 0.1) {
                //     v.vertex.x += sin(_Time.y);
                // }

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // 像素着色器 Pixel Shader
            // 处理颜色
            // 逐像素处理
            fixed4 frag (v2f i) : SV_Target
            {
                // float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, i.uv);
                // fixed4: x y z w
                // col.x = 0; // r g b a
                // fixed4 col = fixed4(uv.x, 1, 1, 1);
                return col;
            }
            ENDCG
        }
    }
}
