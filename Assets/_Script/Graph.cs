using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
	public Transform pointPrefabs;
	Transform[] points;
	[Range(10,100)]public int resolution=10;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		int i = 0;
		points=new Transform[resolution];
		float step=2f/resolution;
		Vector3 scaleFactor=Vector3.one*step;
		Vector3 position=Vector3.zero;
		while (i < points.Length)
		{
			Transform point = Instantiate(pointPrefabs);
			position.x=(i+0.5f)*step-1.0f;
			position.y=position.x*position.x*position.x;
			point.localPosition = position;
			point.localScale=scaleFactor;
			point.SetParent(transform,false);
			points[i]=point;
			i++;
		}
	}
	private void Update() {
		int i=0;
		while(i<points.Length)
		{
			Transform point=points[i];
			Vector3 position=point.localPosition;
			// position.y=position.x*(position.x+Time.time);
			position.y=Mathf.Sin(1*(position.x+Time.time));
			point.localPosition=position;
			i++;
		}
	}
}
