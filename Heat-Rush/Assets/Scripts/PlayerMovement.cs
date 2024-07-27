using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("References")]
	[SerializeField] CharacterController characterController;
	[SerializeField] Transform mainCamera;

	[Header("Movement")]
	float Speed = 10f;
	float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	[Header("Dash")]
	float dashForce = 20f;
	float dashDuration = .25f;
	bool isDashing = false;
	float dashStartTime;

	[Header("Jump")]
	float jumpForce = 5f;

	[Header("Gravity")]
	[SerializeField] Transform groundCheck;
	float groundDistance = 0.4f;
	[SerializeField] LayerMask groundMask;
	bool isGrounded;
	float gravity = -9.81f * 5;
	Vector3 velocity;



	// Update is called once per frame
	void Update()
	{
		ApplyGravity();

		if (!isDashing)
		{
			Move();
		}

		// Handle jump input
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Jump();
		}

		// Handle dash input
		if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
		{
			StartCoroutine(Dash());
		}
		// End dash after dash duration
		if (isDashing && Time.time - dashStartTime >= dashDuration)
		{
			isDashing = false;
		}

	}

	private void ApplyGravity()
	{
		// Check if player is grounded
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		// calculate gravity velocity
		velocity.y += gravity * Time.deltaTime;
		// Apply gravity
		characterController.Move(velocity * Time.deltaTime);
	}

	IEnumerator Dash()
	{
		dashStartTime = Time.time;
		while (Time.time - dashStartTime < dashDuration)
		{
			isDashing = true;
			characterController.Move(transform.forward * dashForce * Time.deltaTime);
			// transform.Translate(transform.forward * dashForce * Time.deltaTime, Space.World);
			yield return null;
		}
	}

	private void Jump()
	{
		velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
	}

	private void Move()
	{
		// get keyboard input
		float verticleInput = Input.GetAxisRaw("Vertical");
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		// create movement vector
		Vector3 direction = new Vector3(horizontalInput, 0, verticleInput).normalized;

		// check if movement vector has a magnitude > 0.1f to move the player
		if (direction.magnitude > 0.1f)
		{
			Debug.Log(verticleInput);
			// get angle to rotate player. convert radian to degree because atan2 returns radian 
			// and add camera angle to move the player relative to camera look direction
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

			// smooth turn angle
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
			targetAngle, ref turnSmoothVelocity, turnSmoothTime);

			// rotate player
			transform.rotation = Quaternion.Euler(0, angle, 0);

			// get movement direction relative to camera
			Vector3 moveDirection = (Quaternion.Euler(0, targetAngle, 0) * Vector3.forward).normalized;

			// move player
			characterController.Move(moveDirection * Speed * Time.deltaTime);
			// transform.Translate(moveDirection * Speed * Time.deltaTime, Space.World);
		}
	}

	void OnCollisionStay(Collision other)
	{
		isGrounded = true;
	}

	void OnCollisionExit(Collision other)
	{
		isGrounded = false;
	}
}
