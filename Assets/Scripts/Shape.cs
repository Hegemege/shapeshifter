using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour
{
	[HideInInspector]
	Rigidbody rb;

	CommonInputManager inputManager;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start()
	{
		inputManager = CommonInputManager.instance;
	}

	void Update()
	{
		Debug.Log(inputManager.HorizontalInput);
		float dt = Time.deltaTime;
		rb.AddTorque(
			inputManager.HorizontalInput * dt * 100,
			0f,
			inputManager.VerticalInput * dt * 100
		);
	}
}
