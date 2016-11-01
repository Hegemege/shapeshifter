using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Rigidbody Target;
	public float Distance;
	public Vector3 CameraOffset;
	public Vector3 LookOffset;
	public float Speed;

	Vector3 destination;

	void Start()
	{
		UpdateDestination();
		LookAtTarget();
	}

	void Update()
	{
		UpdateDestination();

		float sp = Speed * Mathf.Pow(Vector3.Distance(transform.position, destination), 2);
		transform.position = Vector3.MoveTowards(transform.position, destination, sp * Time.deltaTime);

		LookAtTarget();
	}

	void UpdateDestination()
	{
		Vector3 direction = -Target.velocity;
		direction.Normalize();

		destination = new Vector3(
			Target.position.x + (direction.x * Distance) + CameraOffset.x,
			Target.position.y + (direction.y * Distance) + CameraOffset.y,
			Target.position.z + (direction.z * Distance) + CameraOffset.z
		);
	}

	void LookAtTarget()
	{
		Vector3 lookAt = new Vector3(
			Target.position.x + LookOffset.x,
			Target.position.y + LookOffset.y,
			Target.position.z + LookOffset.z
		);
		transform.LookAt(lookAt);
	}
}
