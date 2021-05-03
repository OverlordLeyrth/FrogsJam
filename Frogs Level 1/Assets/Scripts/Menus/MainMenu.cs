using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{
	public GameObject frog_2;
	public GameObject frog_3;
	private CheckFrogs checkFrogs;


	void Start()
	{
		checkFrogs = FindObjectOfType<CheckFrogs>();
	}

	void Update()
	{
		if (checkFrogs.frog_2 == true)
		{
			frog_2.SetActive(true);
		}
		if (checkFrogs.frog_3 == true)
		{
			frog_3.SetActive(true);
		}
	}
	public void NewGame()
	{
		StartCoroutine(StartGame());
		print("start game");

	}

	public void NewGame_2()
	{
		StartCoroutine(StartGame_2());
		print("start game");

	}
	public void NewGame_3()
	{
		StartCoroutine(StartGame_3());
		print("start game");

	}

	private IEnumerator StartGame()
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("Scenes/Frog_1_Level");
	}

	private IEnumerator StartGame_2()
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("Scenes/Frog_2_Level");
	}

	private IEnumerator StartGame_3()
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("Scenes/Frog_3_Level");
	}

	public void QuitGame()
	{
		print("quit game");
		//audioSource.PlayOneShot(impact, 1.0F);
		Application.Quit();

	}
}
