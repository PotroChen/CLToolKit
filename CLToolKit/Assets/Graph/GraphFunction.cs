using UnityEngine;

public static class Graph {
	const float pi = Mathf.PI;
	public delegate Vector3 GraphFunction(float u,float v,float y);
    public static Vector3 SineFunction (float u,float v, float t) {
		Vector3 p = new Vector3();
		p.x = u;
		p.y = Mathf.Sin(pi * (u + t));
		p.z = v;
		return p;
	}

	public static Vector3 MultiSineFunction (float u, float v,float t) {
		Vector3 p = new Vector3();
		p.x = u;
		p.z = v;

		p.y = Mathf.Sin(pi * (u + t));
		p.y += Mathf.Sin(2f * pi * (u + 2f * t)) / 2f;
		p.y  *= 2f / 3f;
		return p;
	}

	public static Vector3 Sine2DFunction (float u, float v, float t) {
		Vector3 p = new Vector3();
		p.x = u;
		p.z = v;

		p.y = Mathf.Sin(pi * (u + t));
		p.y += Mathf.Sin(pi * (v + t));
		p.y *= 0.5f;
		return p;
	}

	public static Vector3 MultiSine2DFunction (float u, float v, float t) {
		Vector3 p = new Vector3();
		p.x = u;
		p.z = v;

		p.y = 4f * Mathf.Sin(pi * (u + v + t * 0.5f));
		p.y += Mathf.Sin(pi * (u + t));
		p.y += Mathf.Sin(2f * pi * (v + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
		return p;
	}

	public static Vector3 RippleFunction (float u, float v, float t) {
		Vector3 p = new Vector3();
		p.x = u;
		p.z = v;

		float d = Mathf.Sqrt(u*u + v*v);
		p.y  = Mathf.Sin(pi * (4f * d - t));
		p.y  /= 1f+10f*d;
		return p;
	}

	public static Vector3 CylinderFunction(float u,float v,float t)
	{
		Vector3 p = new Vector3();

		float r = 1f;
		p.x = r*Mathf.Sin(pi * u);
		p.y = v;
		p.z = r*Mathf.Cos(pi * u);
		return p;
	}

	public static Vector3 WobblyCylinderFunction(float u,float v,float t)
	{
		Vector3 p = new Vector3();

		float r = 1f + Mathf.Sin(6f * pi * u) * 0.2f;
		p.x = r * Mathf.Sin(pi * u);
		p.y = v;
		p.z = r * Mathf.Cos(pi * u);
		return p;
	}

	public static Vector3 TwistingCylinderFunction(float u,float v,float t)
	{
		Vector3 p = new Vector3();

		float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
		p.x = r * Mathf.Sin(pi * u);
		p.y = v;
		p.z = r * Mathf.Cos(pi * u);
		return p;
	}

	public static Vector3 SphereFunction (float u, float v, float t) {
		Vector3 p = new Vector3();
		float r = Mathf.Cos(pi * 0.5f * v);
		p.x = r * Mathf.Sin(pi * u);
		p.y = Mathf.Sin(pi * 0.5f * v);
		p.z = r * Mathf.Cos(pi * u);
		return p;
	}

	public static Vector3 PulsingSphereFunction(float u, float v, float t) {
		Vector3 p = new Vector3();
		float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
		r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
		float s = r * Mathf.Cos(pi * 0.5f * v);
		p.x = s * Mathf.Sin(pi * u);
		p.y = r * Mathf.Sin(pi * 0.5f * v);
		p.z = s * Mathf.Cos(pi * u);
		return p;
	}

	public static Vector3 RingTorusFunction(float u, float v, float t) {
		Vector3 p = new Vector3();
		float r1 = 1f;
		float r2 = 0.5f;
		float s = r2 * Mathf.Cos(pi * v) + r1;
		p.x = s * Mathf.Sin(pi * u);
		p.y = r2 * Mathf.Sin(pi * v);
		p.z = s * Mathf.Cos(pi * u);

		return p;
	}
}
