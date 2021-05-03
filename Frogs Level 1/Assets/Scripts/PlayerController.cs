using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
	public MainMenu menu;
	public float moveSpeed;
	private float activeMoveSpeed;
	public Rigidbody2D myRigidbody;
	private float gravityStore;

	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;
	private bool spawnDust;

	public Vector3 respawnPosition;
	public LevelManager theLevelManager;
	public CheckFrogs checkFrogs;

	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;

	public float invincibilityLength;

	public float invincibilityCounter;

	public SpriteRenderer theSR;

	public static PlayerController instance;

	public bool canMove = true;

	public Transform camTarget;
	public float aheadAmount, aheadSpeed;

	public float hangTime;
	private float hangCounter;

	public float jumpBufferLength;
	private float jumpBufferCount;

	// Start is called before the first frame update

	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		respawnPosition = transform.position;
		theLevelManager = FindObjectOfType<LevelManager>();
		activeMoveSpeed = moveSpeed;
		menu = FindObjectOfType<MainMenu>();
		checkFrogs = FindObjectOfType<CheckFrogs>();
		gravityStore = myRigidbody.gravityScale;
	}

	// Update is called once per frame
	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (!canMove)
		{
			myAnim.SetFloat("Speed", Mathf.Abs(0));
			myRigidbody.velocity = Vector2.zero;
			return;

		}


		if (canMove)
		{

			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
			}
			if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0)
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

			if (Input.GetButtonDown("Jump"))
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
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f)
			{
				myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else
			{
				myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
			}


		}


		myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
		myAnim.SetBool("Grounded", isGrounded);
		//myAnim.SetBool("IsJumping", isGrabbing);



		if (knockbackCounter > 0)
		{
			knockbackCounter -= Time.deltaTime;
			if (transform.localScale.x > 0)
			{
				myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
			}
			else
			{
				myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 1f);
			}


		}

		if (invincibilityCounter > 0)
		{
			invincibilityCounter -= Time.deltaTime;

		}
		if (invincibilityCounter <= 0)
		{
			theLevelManager.invincible = false;
			//theLevelManager.FadeOut();
			theSR.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public void KnockBack()
	{
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillZone")
		{
			FindObjectOfType<AudioManager>().Play("Hurt");
			theLevelManager.Respawn();
		}
		if (other.tag == "Checkpoint")
		{
			respawnPosition = other.transform.position;
			FindObjectOfType<AudioManager>().Play("Checkpoint");
		}

		if (other.tag == "Unlock_2")
		{
			checkFrogs.frog_2 = true;
			Cursor.visible = true;
			SceneManager.LoadScene("Menu");
		}

		if (other.tag == "Unlock_3")
		{
			checkFrogs.frog_3 = true;
			Cursor.visible = true;
			SceneManager.LoadScene("Menu");
		}

		if (other.tag == "Win")
		{
			checkFrogs.frog_3 = true;
			Cursor.visible = true;
			SceneManager.LoadScene("Credits");
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{

		if (other.gameObject.tag == "Water")
		{
			this.gameObject.GetComponent<PlayerController>().enabled = false;
			this.gameObject.GetComponent<Swim>().enabled = true;
		}
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Water")
		{
			this.gameObject.GetComponent<PlayerController>().enabled = false;
			this.gameObject.GetComponent<Swim>().enabled = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Water")
		{
			this.gameObject.GetComponent<PlayerController>().enabled = true;
			this.gameObject.GetComponent<Swim>().enabled = false;
		}
	}
	public void StopPlayer()
	{
		canMove = false;
	}
}

