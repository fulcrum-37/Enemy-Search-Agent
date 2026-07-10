Shader "Unlit/UnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor("Line Color",Color) = (0,0,0,1)
        _BaseColor("Base Color",Color) = (1,1,1,1)
        _RayColor("Ray Color",Color)   = (0.7,1.0,0.2,1)

        _GridSize("Grid Size", Float) = 1
        _LineWidth("Line Width",Float) = 0.05
        _RayHitPosition("Ray Hit Position",Vector) = (0,0,0,0)
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

            struct appdata  //モデルから渡されるデータ
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f      // vertex to fragment
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _LineColor;
            float4 _BaseColor;

            float _GridSize;
            float _LineWidth;

            float4 _RayColor;
            float4 _RayHitPosition;

            v2f vert (appdata v)
            {
                v2f o;

                //mvp変換
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                o.uv = v.uv * _GridSize;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 grid = frac(i.worldPos.xz / _GridSize);

                // step(a,b) → a<=bなら1 a>bなら0  
                float lineX = step(grid.x,_LineWidth);
                float lineY = step(grid.y,_LineWidth);
                // 1+1の場合0~1に制限
                float lineMask = saturate(lineX + lineY);

                float2 currentGrid = floor(i.worldPos.xz / _GridSize);
                float2 hitGrid = floor(_RayHitPosition.xz);

                float hitOn = step(abs(currentGrid.x - hitGrid.x), 0.1) * step(abs(currentGrid.y - hitGrid.y), 0.1);

                // a * (1-t) + b * t
                if(hitOn == 0){
                    return lerp(_BaseColor,_LineColor,lineMask);
                }
                else{
                    return lerp(_RayColor,_LineColor,lineMask);
                }
            }
            ENDCG
        }
    }
}
