using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public Shape StartingShape;
	public List<Shape> Shapes;

	[HideInInspector]
	public Shape CurrentShape;

    private CommonInputManager inputManager;

    void Awake()
    {
        inputManager = CommonInputManager.instance;
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
	}

	Shape NextShape()
	{
		int index = Shapes.IndexOf(CurrentShape) + 1;
		if (index >= Shapes.Count) index = 0;
		return Shapes[index];
	}

	void ShiftTo(Shape toShape)
	{
		foreach (Shape shape in Shapes)
		{
			if (shape.Equals(toShape))
			{
				CurrentShape = shape;
				shape.gameObject.SetActive(true);
			}
			else
			{
				shape.gameObject.SetActive(false);
			}
		}
	}
}
