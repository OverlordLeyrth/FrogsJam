using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	private float activeMoveSpeed;
	private Rigidbody2D myRigidbody;


	public float hangTime;
	private float hangCounter;
	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	public SpriteRenderer theSR;

	public bool canMove = true;

	public float jumpBufferLength;
	private float jumpBufferCount;

	void Start()
	{
		//flying = false;
		myRigidbody = GetComponent<Rigidbody2D>();
		activeMoveSpeed = moveSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (!canMove)
		{

			myRigidbody.velocity = Vector2.zero;
			return;
		}




		if (canMove)
		{
			if ((Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))) && isGrounded)
			{
				myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);

			}
			if ((Input.GetButtonUp("Jump") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))) && myRigidbody.velocity.y > 0)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * .5f);

			}

			if (isGrounded)
			{
				hangCounter = hangTime;
			}
			else
			{
				hangCounter -= Time.deltaTime;
			}

			if (Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow)))
			{
				jumpBufferCount = jumpBufferLength;
				FindObjectOfType<AudioManager>().Play("Jump");
			}

			else
			{
				jumpBufferCount -= Time.deltaTime;
			}
			if (jumpBufferCount >= 0 && hangCounter > 0f)

			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
				jumpBufferCount = 0;
			}

			if (Input.GetAxisRaw("Horizontal") > 0f)
			{
				myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);

				Vector3 scale = transform.localScale;
				transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f)
			{
				myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);

				Vector3 scale = transform.localScale;
				if (scale.x > 0)
				{
					scale.x *= -1;
				}
				transform.localScale = new Vector3(scale.x, scale.y, scale.z);
			}
			else
			{
				myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
			}
		}
	}
}
