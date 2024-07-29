using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatController : MonoBehaviour
{
	public static HeatController Instance; // HeatController 

	float heat = 0;
	public float Heat
	{
		get { return heat; }
		set { heat = value; }
	}

	// Start is called before the first frame update
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Heat = 0;
		}
		if (Instance != null)
		{
			Destroy(gameObject);
		}
	}
}
