using System;
using UnityEngine;

public class GGPaintableShader
{
	public static int _Channel;

	public static int _Position;

	public static int _BrushSize;

	public static int _BrushHardness;

	public static int _Color;

	public static int _Texture;

	public static int _MainTex;

	public static int _IlsandMap;

	public static int _TexelSize;

	static GGPaintableShader()
	{
		_Channel = Shader.PropertyToID("_Channel");
		_Position = Shader.PropertyToID("_Position");
		_BrushSize = Shader.PropertyToID("_BrushSize");
		_BrushHardness = Shader.PropertyToID("_BrushHardness");
		_Color = Shader.PropertyToID("_Color");
		_Texture = Shader.PropertyToID("_Texture");
		_MainTex = Shader.PropertyToID("_MainTex");
		_IlsandMap = Shader.PropertyToID("_IlsandMap");
		_TexelSize = Shader.PropertyToID("_TexelSize");
	}

	public static Shader Load(string shaderName)
	{
		Shader shader = Shader.Find(shaderName);
		if (shader == null)
		{
			throw new Exception("Failed to find shader called: " + shaderName);
		}
		return shader;
	}

	public static Material Build(Shader shader)
	{
		return new Material(shader);
	}

	public static Vector4 IndexToVector(int index)
	{
		switch (index)
		{
		case 0:
			return new Vector4(1f, 0f, 0f, 0f);
		case 1:
			return new Vector4(0f, 1f, 0f, 0f);
		case 2:
			return new Vector4(0f, 0f, 1f, 0f);
		case 3:
			return new Vector4(0f, 0f, 0f, 1f);
		default:
			return default(Vector4);
		}
	}
}
