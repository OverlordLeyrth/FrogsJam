﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : MonoBehaviour
{
	public int healthToGive;
	private LevelManager theLevelManager;

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
		if (other.tag == "Player")
		{
			FindObjectOfType<AudioManager>().Play("BugCrunch");
			theLevelManager.GiveHealth(healthToGive);
			Destroy(gameObject);

		}
	}
}