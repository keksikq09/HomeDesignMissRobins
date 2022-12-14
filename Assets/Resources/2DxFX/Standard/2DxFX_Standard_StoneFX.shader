Shader "2DxFX/Standard/StoneFX"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Distortion ("Distortion", Range(0, 1)) = 0
    _Alpha ("Alpha", Range(0, 1)) = 1
    _Deep ("Alpha", Range(0, 1)) = 1
    _StencilComp ("Stencil Comparison", float) = 8
    _Stencil ("Stencil ID", float) = 0
    _StencilOp ("Stencil Operation", float) = 0
    _StencilWriteMask ("Stencil Write Mask", float) = 255
    _StencilReadMask ("Stencil Read Mask", float) = 255
    _ColorMask ("Color Mask", float) = 15
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float _Distortion;
      uniform float _Alpha;
      uniform float _Deep;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.texcoord.xy = float2(in_v.texcoord.xy);
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.color = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float4 u_xlat1_d;
      float4 u_xlat10_1;
      float2 u_xlat2;
      float u_xlat16_2;
      float2 u_xlat4;
      float u_xlat16_4;
      int u_xlatb4;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = float2((((-in_f.texcoord.xy) * float2(30, 30)) + float2(46.0999985, 46.0999985)));
          float _tmp_dvx_156 = sin(u_xlat0_d.xy);
          u_xlat0_d.xy = float2(_tmp_dvx_156, _tmp_dvx_156);
          u_xlat1_d = ((in_f.texcoord.xyyx * float4(30, 30, 75, 75)) + float4(46.0999985, 46.0999985, 46.0999985, 46.0999985));
          float _tmp_dvx_157 = sin(u_xlat1_d);
          u_xlat1_d = float4(_tmp_dvx_157, _tmp_dvx_157, _tmp_dvx_157, _tmp_dvx_157);
          u_xlat0_d.xy = float2((u_xlat0_d.xy + u_xlat1_d.xy));
          u_xlat0_d.xy = float2((u_xlat1_d.yx + u_xlat0_d.xy));
          u_xlat0_d.xy = float2((u_xlat1_d.zw + u_xlat0_d.xy));
          u_xlat0_d.xy = float2((u_xlat0_d.xy + float2(5, 5)));
          u_xlat4.xy = float2((u_xlat0_d.xy * float2(0.200000003, 0.200000003)));
          u_xlat4.xy = float2(floor(u_xlat4.xy));
          u_xlat0_d.xy = float2(((u_xlat0_d.xy * float2(0.200000003, 0.200000003)) + (-u_xlat4.xy)));
          u_xlat10_1.xyz = tex2D(_MainTex, in_f.texcoord.xy).xyz.xyz;
          u_xlat16_4 = dot(u_xlat10_1.xyz, float3(11.1999998, 8.39999962, 4.19999981));
          u_xlat0_d.x = (u_xlat16_4 + u_xlat0_d.x);
          u_xlat4.x = (u_xlat0_d.x * _Deep);
          u_xlat4.x = floor(u_xlat4.x);
          u_xlat0_d.x = ((u_xlat0_d.x * _Deep) + (-u_xlat4.x));
          u_xlat4.x = ((u_xlat0_d.x * 6) + (-2));
          u_xlat4.x = clamp(u_xlat4.x, 0, 1);
          u_xlat0_d.x = (((-u_xlat0_d.x) * 6) + 2);
          u_xlat0_d.x = clamp(u_xlat0_d.x, 0, 1);
          u_xlat0_d.x = (u_xlat0_d.x + u_xlat4.x);
          u_xlat10_1.xyz = tex2D(_MainTex, in_f.texcoord.yx).xyz.xyz;
          u_xlat16_4 = dot(u_xlat10_1.xyz, float3(11.1999998, 8.39999962, 4.19999981));
          u_xlat2.x = (u_xlat16_4 + u_xlat0_d.y);
          u_xlat4.x = (u_xlat2.x * _Deep);
          u_xlat4.x = floor(u_xlat4.x);
          u_xlat2.x = ((u_xlat2.x * _Deep) + (-u_xlat4.x));
          u_xlat4.x = ((u_xlat2.x * 6) + (-2));
          u_xlat4.x = clamp(u_xlat4.x, 0, 1);
          u_xlat2.x = (((-u_xlat2.x) * 6) + 2);
          u_xlat2.x = clamp(u_xlat2.x, 0, 1);
          u_xlat2.x = (u_xlat2.x + u_xlat4.x);
          u_xlat0_d.x = (u_xlat2.x + u_xlat0_d.x);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1);
          u_xlat2.x = (_Distortion * 64);
          u_xlat2.x = sin(u_xlat2.x);
          u_xlat1_d.x = (u_xlat2.x * 0.001953125);
          u_xlat1_d.y = 0;
          u_xlat2.xy = float2((u_xlat1_d.xy + in_f.texcoord.xy));
          u_xlat10_1 = tex2D(_MainTex, u_xlat2.xy);
          u_xlat16_2 = (u_xlat10_1.z + u_xlat10_1.x);
          u_xlat16_2 = (u_xlat10_1.y + u_xlat16_2);
          u_xlat2.x = ((u_xlat16_2 * 0.266666681) + 0.100000001);
          if((0.600000024<u_xlat2.x))
          {
              u_xlatb4 = 1;
          }
          else
          {
              u_xlatb4 = 0;
          }
          u_xlat2.x = (u_xlatb4)?(0.600000024):(u_xlat2.x);
          if((u_xlat2.x<0.200000003))
          {
              u_xlatb4 = 1;
          }
          else
          {
              u_xlatb4 = 0;
          }
          u_xlat2.x = (u_xlatb4)?(0.200000003):(u_xlat2.x);
          u_xlat0_d.x = (((-u_xlat0_d.x) * 0.125) + u_xlat2.x);
          u_xlat0_d.xyz = float3(((-u_xlat10_1.xyz) + u_xlat0_d.xxx));
          out_f.color.xyz = float3(((float3(_Distortion, _Distortion, _Distortion) * u_xlat0_d.xyz) + u_xlat10_1.xyz));
          out_f.color.w = (u_xlat10_1.w + (-_Alpha));
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Sprites/Default"
}
