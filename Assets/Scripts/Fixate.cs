using UnityEngine;
using System.Collections;

public class Fixate : MonoBehaviour
{

	public Transform Target;

	Vector3 offset;

	void Awake()
	{
		offset = new Vector3(
			transform.position.x - Target.position.x,
			transform.position.y - Target.position.y,
			transform.position.z - Target.position.z
		);
	}

	void Update()
	{
		transform.position = new Vector3(
			Target.position.x + offset.x,
			Target.position.y + offset.y,
			Target.position.z + offset.z
		);
	}
}
