using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public GameObject StartingShape;
	public List<GameObject> Shapes;

	public float RotationSpeed;
	public float ForwardForce;
	public float JumpForce;

	[HideInInspector]
	public GameObject CurrentShape;

	private CommonInputManager inputManager;

	Rigidbody rb;

	void Awake()
	{
		inputManager = CommonInputManager.instance;
		rb = GetComponent<Rigidbody>();
	}

	void Start()
	{
		ShiftTo(StartingShape);
	}

	void Update()
	{
		if (inputManager.SwapInput)
		{
			ShiftTo(NextShape());
		}

		float dt = Time.deltaTime;
		Vector3 asd = new Vector3(0, 0, 0);

		rb.AddTorque(
			-inputManager.HorizontalInput * dt * RotationSpeed,
			0f,
			-inputManager.VerticalInput * dt * RotationSpeed
		);
		rb.AddForce(
			-inputManager.HorizontalInput * dt * ForwardForce,
			0f,
			-inputManager.VerticalInput * dt * ForwardForce
		);
	}

	GameObject NextShape()
	{
		int index = Shapes.IndexOf(CurrentShape) + 1;
		if (index >= Shapes.Count) index = 0;
		return Shapes[index];
	}

	void ShiftTo(GameObject toShape)
	{
		foreach (GameObject shape in Shapes)
		{
			if (shape.Equals(toShape))
			{
				CurrentShape = shape;
				shape.gameObject.SetActive(true);
				transform.rotation = Quaternion.Euler(new Vector3(
					0f,
					transform.rotation.y,
					0f
				));

				rb.AddForce(0f, JumpForce, 0f);
			}
			else
			{
				shape.gameObject.SetActive(false);
			}
		}
	}
}
