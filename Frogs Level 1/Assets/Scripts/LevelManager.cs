using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public PlayerController thePlayer;
	public bool invincible;
	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int healthCount;

	private bool respawning;
	public GameObject deathSplosion;

	void Start()
	{
		thePlayer = FindObjectOfType<PlayerController>();
	}
	void Update()
	{
		if (healthCount <= 0 && !respawning)
		{
			Respawn();
			respawning = true;
		}
	}

	public void Respawn()
	{
		StartCoroutine("RespawnCo");
	}



	public IEnumerator RespawnCo()
	{
		thePlayer.gameObject.SetActive(false);
		Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds(1f);

		healthCount = maxHealth;
		respawning = false;
		UpdateHeartMeter();

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive(true);
	}

	public void HurtPlayer(int damageToTake)
	{

		healthCount -= damageToTake;
		UpdateHeartMeter();

		//FadeIn();
		thePlayer.KnockBack();
		//thePlayer.theSR.color = new Color(1f, 1f, 1f, 0.65f);
	}



	public void GiveHealth(int healthToGive)
	{
		healthCount += healthToGive;
		if (healthCount > maxHealth)
		{
			healthCount = maxHealth;
		}
		UpdateHeartMeter();
	}
	public void UpdateHeartMeter()
	{
		switch (healthCount)
		{
			case 6:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartFull;
				return;

			case 5:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartHalf;
				return;

			case 4:
				heart1.sprite = heartFull;
				heart2.sprite = heartFull;
				heart3.sprite = heartEmpty;
				return;

			case 3:
				heart1.sprite = heartFull;
				heart2.sprite = heartHalf;
				heart3.sprite = heartEmpty;
				return;

			case 2:
				heart1.sprite = heartFull;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;
			case 1:
				heart1.sprite = heartHalf;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;

			case 0:
				heart1.sprite = heartEmpty;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;

			default:
				heart1.sprite = heartEmpty;
				heart2.sprite = heartEmpty;
				heart3.sprite = heartEmpty;
				return;
		}
	}

}
