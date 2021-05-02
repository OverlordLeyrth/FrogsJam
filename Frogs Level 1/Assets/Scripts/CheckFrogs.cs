using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFrogs : MonoBehaviour
{

	public static CheckFrogs instance;
	private PlayerController thePlayer;
	private MainMenu menu;
	public bool frog_2 = false;
	public bool frog_3 = false;
	void Awake()
	{

		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		// Start is called before the first frame update
		void Start()
		{
			thePlayer = FindObjectOfType<PlayerController>();
			menu = FindObjectOfType<MainMenu>();
		}

		// Update is called once per frame
		void Update()
		{
			if (frog_2 == true)
			{
				menu.frog_2.SetActive(true);
			}
			if (frog_3 == true)
			{
				menu.frog_3.SetActive(true);
			}

		}
	}
}
