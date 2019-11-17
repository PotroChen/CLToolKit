using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScriptForGraph : MonoBehaviour {
	public enum GraphFunctionName
	{ 
		Sine,
		Sine2D,
		MultiSine,
		MultiSine2D,
		Ripple,
		Cylinder,
		WobblyCylinder,
		TwistingCylinder,
		Sphere,
		PulsingSphere,
		RingTorus
	}
	public static Graph.GraphFunction[] functions = 
		{
			Graph.SineFunction,
			Graph.Sine2DFunction, 
			Graph.MultiSineFunction,
			Graph.MultiSine2DFunction,
			Graph.RippleFunction,
			Graph.CylinderFunction,
			Graph.WobblyCylinderFunction,
			Graph.TwistingCylinderFunction,
			Graph.SphereFunction,
			Graph.PulsingSphereFunction,
			Graph.RingTorusFunction
		};

	public Transform pointPrefab;

	[Range(10,100)]
	public int resolution = 10;

	public GraphFunctionName function;

	Transform[] points;
	
	void Awake()
	{
		float step = 2f/resolution;
		Vector3 scale = Vector3.one* step;
		points = new Transform[resolution*resolution];

		for (int i = 0; i < points.Length; i++) {
			Transform point = Instantiate(pointPrefab);
			point.localScale = scale;
			point.SetParent(transform, false);
			points[i] = point;
		}
	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time;
		Graph.GraphFunction f = functions[(int)function];
		float step = 2f/resolution;

		for (int i = 0, z = 0; z < resolution; z++) {
			float v = (z + 0.5f) * step - 1f;
			for (int x = 0; x < resolution; x++, i++) {
				float u = (x + 0.5f) * step - 1f;
				points[i].localPosition = f(u, v, t);
			}
		}
	}
}
