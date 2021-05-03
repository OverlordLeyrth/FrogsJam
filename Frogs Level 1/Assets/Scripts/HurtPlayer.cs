using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
	private LevelManager theLevelManager;
	public int damageToGive;
	public GameObject impactHurt;
	// Start is called before the first frame update
	void Start()
	{
		theLevelManager = FindObjectOfType<LevelManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && theLevelManager.invincible != true)
		{
			// theLevelManager.Respawn();
			ScreenShakeController.instance.StartShake(.25f, .07f);
			theLevelManager.HurtPlayer(damageToGive);
			FindObjectOfType<AudioManager>().Play("Hurt");

			//Instantiate(impactHurt, other.transform.position, other.transform.rotation);
		}
	}
}
