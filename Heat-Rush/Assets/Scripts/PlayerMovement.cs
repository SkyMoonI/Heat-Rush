using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] CharacterController characterController;
	[SerializeField] Transform mainCamera;
	float Speed = 5f;

	float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;
	// Update is called once per frame
	void Update()
	{
		// get keyboard input
		float verticleInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		// create movement vector
		Vector3 direction = new Vector3(horizontalInput, 0, verticleInput).normalized;

		// check if movement vector has a magnitude > 0.1f to move the player
		if (direction.magnitude > 0.1f)
		{
			// get angle to rotate player. convert radian to degree because atan2 returns radian 
			// and add camera angle to move the player relative to camera look direction
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

			// smooth turn angle
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
			targetAngle, ref turnSmoothVelocity, turnSmoothTime);

			// rotate player
			transform.rotation = Quaternion.Euler(0, angle, 0);

			// get movement direction relative to camera
			Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

			// move player
			characterController.Move(moveDirection * Speed * Time.deltaTime);
		}
	}
}
