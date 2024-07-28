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
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{

		if (heat < 100)
		{
			heat += Time.deltaTime;
		}

		Debug.Log(heat);
	}
}
