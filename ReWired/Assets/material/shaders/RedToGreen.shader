Shader "Custom/RedToGreenWithPlayerCheckBool"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IsPlayerOnObject ("Is Player On Object", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
        LOD 200
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            float _IsPlayerOnObject;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                
                // Check if the player is on the object
                if (_IsPlayerOnObject > 0.5)
                {
                    // Check if red is the highest component
                    if (color.r > color.g && color.r > color.b && color.g < .5)
                    {
                        // Swap red and green components
                        float temp = color.r;
                        color.r = color.g;
                        color.g = temp;
                    }
                }
                
                return color;
            }
            ENDCG
        }
    }
}