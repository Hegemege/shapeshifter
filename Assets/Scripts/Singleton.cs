using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour
{

	public static Singleton instance = null;

	void Awake()
	{
		// Create new static instance if not already existing
		if (instance == null) instance = this;
		else if (!instance.Equals(this)) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
}
