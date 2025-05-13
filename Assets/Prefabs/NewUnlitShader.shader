Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _ColorTop("Top Color", Color) = (0.1, 0.1, 0.1, 1)
        _ColorBottom("Bottom Color", Color) = (0, 0, 0, 1)
        _DepthRange("Depth Range", Float) = 10
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            fixed4 _ColorTop;
            fixed4 _ColorBottom;
            float _DepthRange;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float t = saturate(i.worldPos.y / _DepthRange);
                fixed4 color = lerp(_ColorBottom, _ColorTop, t);
                return color;
            }
            ENDCG
        }
    }
}
