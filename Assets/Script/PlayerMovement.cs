using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Input")]
	float horizontalInput;
	float verticalInput;
	public bool flipX = false;
	public bool flipY = false;
	
	[Header("Movement Attributes")]
	public float moveSpeed;
	public float groundDrag;
	public float airMultiplier;
	public float airDrag;
	Vector3 moveDirection;
	public float rotationSpeed;
	public float gravityScale;
	private float normalGravityScale;
	private float jumpGravityScale;
	private float jumpTime = 0f;
	private float jumpTimeGoal = 0.75f;

	float wasSpeed = 0.0f;

	[Header("Grabbing Movement Attributes")]
	public float grabMoveSpeed;

	[HideInInspector]
	public float currentMoveSpeed;


	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask groundMask;
	public bool grounded;
	bool wasGrounded;

	[Header("Jump Attributes")]
	public float jumpForce;
	public float jumpCooldown;
	bool readyToJump = true;
	bool jumpLifted = false; // After jump button pressed player must lift off the space bar before they can press again to float

	[Header("Program Needed")]
	public GameObject characterRepresentation;
	//public Animator animator;
	Rigidbody rb;
	public ParticleSystem dust;

	public bool rotatePlayerToMove = true;
	
	public bool canFloat = false;

	private bool checkJump = false;

	private void Start()
	{
		wasGrounded = true;
		playerHeight = playerHeight * 0.5f + 0.3f;

		//orientation = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
		currentMoveSpeed = moveSpeed;
		normalGravityScale = gravityScale;
	}

	private void GetInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
			
		if(flipX)
		{
			horizontalInput *= -1.0f;
		}

		if(flipY)
		{
			verticalInput *= -1.0f;
		}
	}

	void Update()
	{
		GetInput();


		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, groundMask);
		//animator.SetBool("Grounded", grounded);
		
		if (!wasGrounded && grounded)
		{
			//Play landing sound
			CreateDust();
		}
		wasGrounded = grounded;

		if(wasSpeed <= 0.5f && rb.velocity.magnitude >= 0.5f)
		{
			if(grounded)
			{
				CreateDust();
			}
		}
		wasSpeed = rb.velocity.magnitude;

		//animator.SetFloat("MoveSpeed", rb.velocity.magnitude);

		if (grounded)
		{
			rb.drag = groundDrag;
		}
        else
		{
			rb.drag = airDrag;
		}
	}

	void FixedUpdate()
	{
		MovePlayer();
	}

	private void MovePlayer()
	{
		moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

		if(grounded)
		{
			rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);			
		}
		else
		{
			rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f * airMultiplier, ForceMode.Force);
		}

		// face character representation towards movement of direction
		if(moveDirection != Vector3.zero && rotatePlayerToMove)
		{
			// Calculate the target rotation based on the movement direction
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

			// Interpolate the current rotation towards the target rotation
			//characterRepresentation.transform.rotation = Quaternion.Slerp(characterRepresentation.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}

		
		rb.AddForce(transform.up * -1.0f * gravityScale * 10f, ForceMode.Force);
	}

	void CreateDust()
	{
		dust.Play();
	}
}
