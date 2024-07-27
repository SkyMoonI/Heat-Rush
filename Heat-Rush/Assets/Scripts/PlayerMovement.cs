using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	float VerticleInput;
	float HorizontalInput;
	float Speed = 5f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		VerticleInput = Input.GetAxis("Vertical");
		HorizontalInput = Input.GetAxis("Horizontal");
		transform.Translate(HorizontalInput * Speed * Time.deltaTime, 0, VerticleInput * Speed * Time.deltaTime);
	}
}
