using System;
using UnityEngine;

public class RocketDemo : MonoBehaviour
{
	[Serializable]
	public class Circle
	{
		public Vector3 position;

		public float radius;
	}

	[Serializable]
	public class TangentDistanceFactors
	{
		public float tangentMultDistance = 0.5f;

		public float radiusTangentMultDistance = 0.5f;
	}

	public enum DrawMode
	{
		DrawModeCircles,
		DrawModePath
	}

	public struct Tangents
	{
		public float gamaRad;

		public float betaRad;

		public float alphaRad;

		public float tan1AngleRad;

		public float tan2AngleRad;

		public Circle c1;

		public Circle c2;

		public Vector3 c1Tan1;

		public Vector3 c2Tan1;

		public Vector3 c1Tan2;

		public Vector3 c2Tan2;
	}

	public Circle c1 = new Circle();

	public Circle c2 = new Circle();

	public float angleOffset;

	[SerializeField]
	public TangentDistanceFactors tangentFactors = new TangentDistanceFactors();

	public bool controlPoint2LookingToStart;

	public DrawMode drawMode;

	public static Tangents FindOuterTangents(Circle c1, Circle c2)
	{
		Tangents tangents = default(Tangents);
		bool flag = false;
		if (c1.radius > c2.radius)
		{
			Circle circle = c1;
			c1 = c2;
			c2 = circle;
			flag = true;
		}
		tangents.c1 = c1;
		tangents.c2 = c2;
		Vector3 vector = c2.position - c1.position;
		vector.z = 0f;
		float magnitude = vector.magnitude;
		tangents.gamaRad = Mathf.Atan2(c2.position.y - c1.position.y, c2.position.x - c1.position.x);
		tangents.betaRad = Mathf.Asin((c2.radius - c1.radius) / magnitude);
		tangents.alphaRad = tangents.gamaRad + tangents.betaRad;
		tangents.tan1AngleRad = tangents.alphaRad;
		tangents.tan2AngleRad = (float)Math.PI + (tangents.gamaRad - tangents.betaRad);
		Vector3 a = Quaternion.AngleAxis(tangents.tan1AngleRad * 57.29578f, Vector3.forward) * Vector3.up;
		Vector3 a2 = Quaternion.AngleAxis(tangents.tan2AngleRad * 57.29578f, Vector3.forward) * Vector3.up;
		if (flag)
		{
			tangents.c2Tan1 = c1.position + a * c1.radius;
			tangents.c1Tan1 = c2.position + a * c2.radius;
			tangents.c2Tan2 = c1.position + a2 * c1.radius;
			tangents.c1Tan2 = c2.position + a2 * c2.radius;
		}
		else
		{
			tangents.c1Tan1 = c1.position + a * c1.radius;
			tangents.c2Tan1 = c2.position + a * c2.radius;
			tangents.c1Tan2 = c1.position + a2 * c1.radius;
			tangents.c2Tan2 = c2.position + a2 * c2.radius;
		}
		return tangents;
	}
}
