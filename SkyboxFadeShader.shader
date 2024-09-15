Shader "Custom/SkyboxPanoramicFadeShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _BlendTex ("Blend Texture", 2D) = "white" {}
        _Blend ("Blend Factor", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Opaque" }
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
                float3 worldDir : TEXCOORD0; // World direction vector
            };

            sampler2D _MainTex;
            sampler2D _BlendTex;
            float _Blend;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // Calculate the world direction in view space
                o.worldDir = normalize(mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Latitude-Longitude (Equirectangular) Mapping

                // Calculate the longitude (X-axis) and latitude (Y-axis) for spherical UV mapping
                float2 latlong;

                // Longitude: Horizontal angle (theta) wraps around the Y-axis.
                // We adjust this by rotating 90 degrees clockwise and flipping the direction.
                latlong.x = (atan2(i.worldDir.z, -i.worldDir.x) / (2 * UNITY_PI)) + 0.5;
                latlong.x -= 0.25; // Offset by 90 degrees clockwise (0.25 corresponds to 90 degrees or PI/2 radians)

                // Latitude: Vertical angle (phi), which ranges from -1 to 1 and wraps along the Y-axis.
                latlong.y = asin(i.worldDir.y) / UNITY_PI + 0.5;

                // Ensure latlong.x is wrapped correctly around [0, 1] in case the subtraction goes below 0
                latlong.x = fmod(latlong.x + 1.0, 1.0);

                // Sample both textures using the calculated latlong UVs
                fixed4 col1 = tex2D(_MainTex, latlong);
                fixed4 col2 = tex2D(_BlendTex, latlong);

                // Blend between the two textures
                return lerp(col1, col2, _Blend);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
