using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
	float rotationX;
	float rotationY;
	float sensetivity = 15f;


	// Update is called once per frame
	void Update()
	{
		rotationX += Input.GetAxis("Mouse X") * sensetivity;
		rotationY += Input.GetAxis("Mouse Y") * sensetivity;
		rotationY = Mathf.Clamp(rotationY, -90, 90);
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
}
