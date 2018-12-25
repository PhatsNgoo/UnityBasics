using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
	public Transform pointPrefabs;
	Transform[] points;
	static GraphFunction[] functions = { SineFunc,Sine2DFunc, MultiSineFunc };
	[Range(10, 100)] public int resolution = 10;
	[Range(0, 2)] public int function;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		points = new Transform[resolution * resolution];
		float step = 2f / resolution;
		Vector3 scaleFactor = Vector3.one * step;
		Vector3 position = Vector3.zero;
		for (int i = 0, z = 0; z < resolution; z++)
		{
			position.z = (z + 0.5f) * step - 1.0f;
			for (int x = 0; x < resolution; x++, i++)
			{
				Transform point = Instantiate(pointPrefabs);
				position.x = (x + 0.5f) * step - 1.0f;
				position.y = position.x * position.x * position.x;
				point.localPosition = position;
				point.localScale = scaleFactor;
				point.SetParent(transform, false);
				points[i] = point;
			}
		}
	}
	private void Update()
	{
		GraphFunction func = functions[function];
		for (int i = 0; i < points.Length; i++)
		{
			Transform point = points[i];
			Vector3 position = point.localPosition;
			// position.y=position.x*(position.x+Time.time);
			position.y = func(position.x, position.z, Time.time);
			point.localPosition = position;
		}
	}
	const float pi=Mathf.PI;
	static float SineFunc(float x, float z, float t)
	{
		return Mathf.Sin(pi * (x + t));
	}
	static float Sine2DFunc(float x,float z,float t)
	{
		return Mathf.Sin(pi*(x+z+t));
	}
	static float MultiSineFunc(float x, float z, float t)
	{
		float y = Mathf.Sin(pi * (x + t));
		y += Mathf.Sin(2f * pi * (x + 3f * t)) / 2f;
		y *= 2f / 3f;
		return y;
	}
}
