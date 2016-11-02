using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour
{
	public ShapeType Type;

	public float RotationSpeed;
	public float ForwardForce;

	public bool JumpOnShift;
	public Vector3 JumpForce;

	public float Mass;
	public Vector3 CenterOfMass;

	public float Drag;
	public float AngularDrag;
}
