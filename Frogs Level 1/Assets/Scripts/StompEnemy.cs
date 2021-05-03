using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
	public GameObject impactStomp;
	private Rigidbody2D playerRigidbody;
	//public AudioSource stompSound;
	//public Transform dropPoint;
	//public bool drops;
	//public GameObject theDrops;
	//public DropEnemy dropEnemy;
	//public int enemyHealth = 1;

	public float bounceForce;
	// Start is called before the first frame update
	void Start()
	{
		playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
		//dropEnemy = FindObjectOfType<DropEnemy>();
	}

	// Update is called once per frame
	void Update()
	{
		// if(enemyHealth == 0 && dropEnemy.drops)
		// {
		//          dropEnemy.DropItems();
		//          enemyHealth = 1;
		//  }
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			ScreenShakeController.instance.StartShake(.25f, .07f);
			other.gameObject.SetActive(false);
			Instantiate(impactStomp, other.transform.position, other.transform.rotation);
			playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
			FindObjectOfType<AudioManager>().Play("Hit");
			// enemyHealth = 0;
		}

	}
}
