Shader "Custom/416"
{
    Properties
    {
		[NoScaleOffset]
        _MainTex ("颜色贴图", 2D) = "grey" {}
		_DarkPow("暗色",Range(0,1.0)) = 0.5
			[NoScaleOffset]
		_BumpMap("法线贴图",2D) = "bump"{}
		_BumpScale("法线贴图强度",Range(-2.0,2.0)) = -1.0
		[NoScaleOffset]_MaskTex("RGB-遮罩", 2D) = "black" {}
		_RColor("红色通道范围",Color) = (1,0,0,1)
		_GColor("绿色通道范围",Color) = (0,1,0,1)
		_BColor("蓝色通道范围",Color) = (0,0,1,1)
			[NoScaleOffset]
		_MSTTex("金属度R光滑度G闭塞B", 2D) = "grey" {}
		[NoScaleOffset]
		_OTETex("半透明范围R透光通道G自发光B", 2D) = "black" {}
		_EColor("自发光颜色",Color) = (0,1,0,1)

		_FaceRatioColor("轮廓光颜色(A轮廓光范围)",Color) = (0.5,0.5,0.5,0.5)
			[NoScaleOffset][HDR]
		_MatCapTex("MatCap鱼眼HDIR贴图", 2D) = "black" {}





    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100



		   //===================创建阴影pass===================
	pass
	{
		   //---------------阴影模式
		   Tags{ "LightMode" = "ShadowCaster" }

		   CGPROGRAM
		   #pragma vertex vert
		   #pragma fragment frag

			   //---------------创建阴影
		   #pragma multi_compile_shadowcaster

		   #include "UnityCG.cginc"

			   struct appdata
		   {
			   float4 vertex : POSITION;
			   half3 normal:NORMAL;
		   };

		   struct v2f
		   {
			   //---------------申请阴影数据结构
			   V2F_SHADOW_CASTER;
		   };


		   v2f vert(appdata v)
		   {
			   v2f o;
			   //---------------放入阴影生成模块
			   TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			   return o;
		   }

		   fixed4 frag(v2f i) : SV_TARGET
		   {
			   //---------------渲染阴影
			   SHADOW_CASTER_FRAGMENT(i)
		   }
			   ENDCG
	   }









        Pass
        {
			Cull Off


		Tags{"LightMode" = "ForwardBase"}
            CGPROGRAM
			#pragma multi_compile_fwdbase
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
			#include "Lighting.cginc"
		//获取阴影命令集
		#include "AutoLight.cginc" 

            struct appdata
            {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					//获取表面法线
					float3 normal:NORMAL;
					//获取表面切线
					float4 tangent:TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
				float4 T2W0:TEXCOORD1;
				float4 T2W1:TEXCOORD2;
				float4 T2W2:TEXCOORD3;
				float3 TtoV0:TEXCOORD4;
				float3 TtoV1:TEXCOORD5;
				float3 TtoV2:TEXCOORD6;
				UNITY_FOG_COORDS(8)
				SHADOW_COORDS(9)
            };


            sampler2D _MainTex;
			float _DarkPow;
			sampler2D _BumpMap;
			float _BumpScale;
			sampler2D _MaskTex;
			float4 _RColor;
			float4 _GColor;
			float4 _BColor;
		
			
			sampler2D _MSTTex;
			half4 _EColor;
			sampler2D _OTETex;
			half4 _MainColor;
			half4 _FaceRatioColor;
			sampler2D _MatCapTex;
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
				//世界法线
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				//世界切线
				float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				//世界副切线
				float3 worldBinormal = cross(worldNormal, worldTangent)*v.tangent.w;
				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;


				//获取模型到切线转换变量  rotation
				TANGENT_SPACE_ROTATION;


				//转递切线到视角的转换变量组
				o.TtoV0 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[0].xyz));
				o.TtoV1 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[1].xyz));
				o.TtoV2 = normalize(mul(rotation, UNITY_MATRIX_IT_MV[2].xyz));




				//构建变换矩阵
				//z轴是法线方向(n)，x轴是切线方向(t)，y轴可由法线和切线叉积得到，也称为副切线（bitangent, b）
				o.T2W0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.T2W1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.T2W2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);



                UNITY_TRANSFER_FOG(o,o.pos);
				//获取模型上的阴影
				TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

				//获取法线贴图
					float4 Normaltex = tex2D(_BumpMap, i.uv);
					//法线贴图0~1转-1~1
					float3 tangentNormal = UnpackNormal(Normaltex);
					//乘以凹凸系数
					tangentNormal.xy *= _BumpScale;
					//向量点乘自身算出x2+y2，再求出z的值
					tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
					//向量变换只需要3*3
					float3x3 T2WMatrix = float3x3(i.T2W0.xyz,i.T2W1.xyz,i.T2W2.xyz);
					//法线从切线空间到世界空间
					float3 worldNormal = mul(T2WMatrix,tangentNormal);

					//获取顶点世界坐标
					float3 WordPos = float3(i.T2W0.w, i.T2W1.w, i.T2W2.w);

			float3 lightDir = normalize( _WorldSpaceLightPos0.xyz);

			//摄像机方向
			fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz- WordPos);
			//面比率渐变
			fixed Ramp_FaceRatio = dot(worldNormal, viewDir);

			fixed UpRatio =pow( saturate( dot(worldNormal, float3(0, 1, 0))+1),2);

			fixed ref_FaceRatio = (1-Ramp_FaceRatio) * UpRatio;


			//使用反向光源方向与法线求高光反射
			fixed3 reflectDir = normalize(reflect(-lightDir, worldNormal));

			//反射光线与视角点乘
			fixed 	Ramp_Specular =dot(reflectDir,viewDir);






			//获取阴影最终结果
			fixed shadow =SHADOW_ATTENUATION(i);



			//获取颜色纹理
			fixed4 col = tex2D(_MainTex, i.uv);

			//混合变色
			fixed4 mask = tex2D(_MaskTex, i.uv);
			col.rgb = lerp(col.rgb, col.rgb* _RColor.rgb*1.3, mask.r);
			col.rgb = lerp(col.rgb, col.rgb*_GColor.rgb*1.3, mask.g);
			col.rgb = lerp(col.rgb, col.rgb*_BColor.rgb*1.3, mask.b);



			//获取材质效果纹理
				fixed4 mro = tex2D(_MSTTex, i.uv);
				fixed4 ste = tex2D(_OTETex, i.uv);

				//光照方向渐变
				half Ramp_Lighting =dot(worldNormal,lightDir);


				//合成光照渐变
				float4 outcol = lerp(col*pow((1 - mro.g), _DarkPow + 1), col, saturate(Ramp_Lighting));
				
				//混合漫反射强度
				outcol *= ((1 - mro.g)*1.2+0.2);
				
				//面比率渐变色提高立体感
				outcol *=pow(Ramp_FaceRatio,5)*4*(mro.g*2)+ (1-mro.g)*0.4;



			 //---------------获取灯光颜色
			 fixed3 lightcolor = _LightColor0.rgb;

			 //光照着色
			 outcol.rgb *= lightcolor;


			 //---------------获取环境色
			 fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
			 //合成阴影 AO 与环境光
			 outcol.rgb *= saturate(shadow + mro.b* ambient);

			 //合成半透明光照
			 outcol.rgb +=  pow(col,10) *10* ste.r* ste.g*saturate(1- Ramp_Lighting)*lightcolor;


			 //调节高光范围与强度
			 Ramp_Specular = pow(Ramp_Specular, mro.g * 50 + 1)*mro.r;

			 //合成高光
			 outcol.rgb += saturate(Ramp_Specular * 2)*lightcolor*(col+0.5);

			 //切线空间转视角空间 向量变换只需要3*3
			 float3x3 TtoVMatrix = float3x3(i.TtoV0.xyz, i.TtoV1.xyz, i.TtoV2.xyz);

			 //法线从切线空间到视角空间
			 float3 _viewNormal = mul(TtoVMatrix, tangentNormal);


			 //视角空间法线 由-1~1转换为0~1；
			 _viewNormal = saturate(_viewNormal*0.5 + 0.5);

			 //依据新UV获取贴图颜色
			 float4 matcap = tex2Dlod(_MatCapTex, float4(_viewNormal.xy, 0, (1-mro.g)*15));

			 //合成轮廓光
			 outcol.rgb += pow(ref_FaceRatio,mro.g  +2)*_FaceRatioColor.rgb*(Ramp_FaceRatio + _FaceRatioColor.a)*(col+0.5);
			 
			 //合成反射
			 outcol.rgb += matcap * mro.g*col*saturate(ref_FaceRatio + _FaceRatioColor.a);

			 //合成自发光
			 outcol =lerp(outcol,col, ste.b)+ _EColor* _EColor.a* ste.b*5;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, outcol);
				//继承透明度
				outcol.a = col.a;
				//剔除透明
				clip(col.a-0.5);

				//输出颜色
				return outcol;
            }
            ENDCG
        }

    }
}
