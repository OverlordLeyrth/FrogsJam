using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{

	private Rigidbody2D rb_;
	private Coroutine floatingCo_;
	private Coroutine dashCo_;
	private float horizontal_;
	private float vertical_;
	private const float MoveLimiter = 0.7f;

	public float runSpeed = 20.0f;
	public float floatLerp;

	[Header("Idle Movement Vars")]
	public float idleAnimLer;
	public float delayBeforeEachMove;
	private float idleFloating_;
	private bool idleFlSwitch_;
	public float normalGravity;

	private Animator myAnim;

	[Header("Dash Stuff")]
	public float dashStrength;
	public float dashCoolDown;
	[SerializeField] private bool canDash_ = true;

	public float jetpackForce = 10.0f;
	public float flyingSpeed = 1f;

	public bool inWater;

	private void Awake()
	{
		rb_ = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		this.enabled = false;
	}

	void Start()
	{
		inWater = false;
	}

	public void Update()
	{
		myAnim.SetBool("Water", inWater);
		// Gives a value between -1 and 1
		horizontal_ = Input.GetAxisRaw("Horizontal"); // -1 is left
		vertical_ = Input.GetAxisRaw("Vertical"); // -1 is down
		if (idleFlSwitch_)
		{
			idleFloating_ = Mathf.Lerp(idleFloating_, -1, idleAnimLer);

		}
		else
		{
			idleFloating_ = Mathf.Lerp(idleFloating_, 1, idleAnimLer);
		}

		if (Input.GetButtonDown("Dash"))
		{
			//	myAnim.SetBool("dashing", canDash_);
			if ((horizontal_ != 0 || vertical_ != 0) && canDash_)
			{
				rb_.AddForce(dashStrength * rb_.velocity.normalized);
				//FindObjectOfType<AudioManager>().Play("Dash");
				dashCo_ = StartCoroutine(DashCoolDown());
			}
		}

		if (Input.GetButtonDown("Dash"))
		{
			//myAnim.SetBool("dashing", canDash_);
			if ((horizontal_ != 0 || vertical_ != 0) && canDash_)
			{
			}
		}

	}

	private IEnumerator DashCoolDown()
	{
		canDash_ = false;
		yield return new WaitForSeconds(dashCoolDown);
		canDash_ = true;
	}

	private void FixedUpdate()
	{
		if (horizontal_ != 0 && vertical_ != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal_ *= MoveLimiter;
			vertical_ *= MoveLimiter;
		}

		Float();
	}

	private void Float()
	{
		Vector2 normalVelo;
		if (vertical_ == 0 && horizontal_ == 0)
		{
			if (floatingCo_ != null)
			{
				StopCoroutine(Floater(delayBeforeEachMove));
			}
			else
			{
				floatingCo_ = StartCoroutine(Floater(delayBeforeEachMove));
			}
			rb_.AddForce(new Vector2(0, jetpackForce));
			normalVelo = new Vector2(horizontal_ * runSpeed, idleFloating_);
		}

		else
		{
			normalVelo = new Vector2(horizontal_ * runSpeed, vertical_ * runSpeed);
		}

		rb_.velocity = Vector2.Lerp(rb_.velocity, normalVelo, floatLerp);
	}

	private IEnumerator Floater(float delay)
	{
		yield return new WaitUntil(ReturnSpeed);
		yield return new WaitForSeconds(delay);

		if (idleFloating_ > 0)
		{
			idleFlSwitch_ = true;
		}
		else
		{
			idleFlSwitch_ = false;
		}

		floatingCo_ = null;
	}

	private bool ReturnSpeed()
	{
		return vertical_ == 0 && horizontal_ == 0;
	}



	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Water")
		{
			//FindObjectOfType<AudioManager>().Play("ShadowState");
			//myAnim.SetBool("Water", true);
			inWater = true;
		}
	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Water")
		{
			this.gameObject.GetComponent<PlayerController>().enabled = false;
			this.gameObject.GetComponent<Swim>().enabled = true;
			rb_.gravityScale = 0;
			inWater = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Water")
		{
			//myAnim.SetBool("Water", false);
			//FindObjectOfType<AudioManager>().Stop("ShadowState");
			rb_.gravityScale = normalGravity;
			this.gameObject.GetComponent<PlayerController>().enabled = true;
			this.gameObject.GetComponent<Swim>().enabled = false;
			inWater = false;
		}
	}
}