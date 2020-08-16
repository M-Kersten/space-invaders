Shader "Custom/PerspectiveGround"
{
    Properties
    {        
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DepthNoise("Noise depth map", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionTex ("Emission", 2D) = "black" {}
        _EmissionColor("Emission color", Color) = (1,1,1,1)
        _AnimationSpeed("Animation speed", Range(0,100)) = 0
        _OffsetSize("Offset size", Range(0,100)) = 0
        _Curvature("Curvature", Range(0, 0.01)) = 0
        _CurvatureHorizontal("Horizontal Curvature", Range(0, 0.01)) = 0
        _CurvatureSpeed("Curvature speed", Range(0, 1)) = 0
        _CurvatureSpeedHorizontal("Horizontal Curvature speed", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DepthNoise;
            float2 uv_EmissionTex;
            float3 viewDir;
            float3 customColor;
        };
        
        sampler2D _MainTex;
        sampler2D _DepthNoise;
        sampler2D _EmissionTex;
        fixed4 _Color;
        fixed4 _EmissionColor;
        float _AnimationSpeed;
        float _OffsetSize;
        half _Glossiness;
        half _Metallic;
        float _Curvature;
        float _CurvatureHorizontal;
        float _CurvatureSpeed;
        float _CurvatureSpeedHorizontal;
                 
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v, out Input o) 
        {
            // get Unity input to use for perspective shifting
            UNITY_INITIALIZE_OUTPUT(Input,o);
            
            // calculate hill height
            float4 oPos = UnityObjectToClipPos(v.vertex);
            float2 offset = float2(0, _Time.x * -_AnimationSpeed);
            float4 tex = tex2Dlod(_DepthNoise, float4(v.texcoord.xy + offset,0,0));
            
            // apply hill height
            v.vertex.z += tex * _OffsetSize;

            // calculate horizontal and vertical perspective
            float4 vv = mul(unity_ObjectToWorld, v.vertex);
            float4 vv2 = mul(unity_ObjectToWorld, v.vertex);
            vv.yz -= _WorldSpaceCameraPos.yz;
            vv2.x -= _WorldSpaceCameraPos.x;
            vv = float4(0.0f, ((vv.z * vv.z) + (vv.x * vv.x)) * sin(_Time.y * _CurvatureSpeed) * _Curvature , 0.0f, 0.0f);
            vv2 = float4(((vv2.z * vv2.z) + (vv2.x * vv2.x)) * sin(_Time.y * _CurvatureSpeedHorizontal) * _CurvatureHorizontal, 0.0f, 0.0f, 0.0f);
            
            // apply perspective to vertices
            v.vertex += mul(unity_WorldToObject, vv);
            v.vertex += mul(unity_WorldToObject, vv2);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // animate the road moving with offset based on animationspeed
            float2 offset = float2(0, _Time.x * -_AnimationSpeed);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + offset);
            c += tex2D(_EmissionTex, IN.uv_MainTex) * _Color;
            
            o.Emission = tex2D(_EmissionTex, IN.uv_MainTex + offset) * _EmissionColor;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
