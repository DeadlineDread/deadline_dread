Shader "Custom/BloodEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _BloodTex ("Blood Texture", 2D) = "white" {}
        _Threshold ("Threshold", Range(0, 1)) = 0.5
        _Distortion ("Distortion", Range(0, 1)) = 0.1
        _Intensity ("Intensity", Range(0, 1)) = 1.0
        _Speed ("Speed", Range(0, 1)) = 0.5
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
            sampler2D _NoiseTex;
            sampler2D _BloodTex;
            float _Threshold;
            float _Distortion;
            float _Intensity;
            float _Speed;
            
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
                
                float noiseValue = tex2D(_NoiseTex, i.uv).r;
                float distortion = smoothstep(0, _Distortion, noiseValue);
                
                float bloodValue = tex2D(_BloodTex, i.uv + distortion * 0.1).r;
                fixed4 bloodColor = lerp(color, bloodValue, _Intensity);
                
                return bloodColor;
            }
            ENDCG
        }
    }
}
