using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public PlayerController Player;
	public Transform LookPoint;
	public Transform CameraPoint;
	public float Speed;

	void Start()
	{
		Vector3 playerPos = Player.transform.position;

		transform.position = CameraPoint.position;
		transform.LookAt(LookPoint);
	}

	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, CameraPoint.position, Speed * Time.deltaTime);

		transform.LookAt(LookPoint);
	}
}
