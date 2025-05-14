Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _ColorTop("Top Color", Color) = (0.1, 0.1, 0.1, 1)
        _ColorBottom("Bottom Color", Color) = (0, 0, 0, 1)
        _FogColor("Fog Color", Color) = (0.2, 0.2, 0.2, 1)
        _DepthRange("Depth Range", Float) = 1
        _FogStrength("Fog Strength", Float) = 1
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
            fixed4 _FogColor;
            float _DepthRange;
            float _FogStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Depth gradient from world Y
                float t = saturate(i.worldPos.y / _DepthRange);
                fixed4 baseColor = lerp(_ColorBottom, _ColorTop, t);

                // Fog intensifies as Y decreases (goes deeper)
                float fogFactor = saturate(0.1 - t) * _FogStrength;
                baseColor = lerp(baseColor, _FogColor, fogFactor);

                return baseColor;
            }
            ENDCG
        }
    }
}
