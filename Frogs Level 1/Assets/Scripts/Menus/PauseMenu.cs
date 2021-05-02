using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
	public string mainMenu;
	public GameObject thePauseMenu;
	//public GameObject UIToDeactivate;
	public static bool GameIsPaused = false;

	void Start()
	{
		Cursor.visible = false;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			print("PauseMenu: Pressed Escape!");
			if (GameIsPaused)
			{
				print("PauseMenu: Game is Paused, resuming!");
				Cursor.visible = false;
				ResumeGame();
			}
			else if (!GameIsPaused)
			{
				print("PauseMenu: Pausing Game!");
				Cursor.visible = true;
				PauseGame();
			}
		}
	}
	public void ResumeGame()
	{
		print("PauseMenu: Exec ResumeGame()!");
		thePauseMenu.SetActive(false);
		//UIToDeactivate.SetActive(true);
		Time.timeScale = 1f;
		GameIsPaused = false;
		AudioListener.volume = 1f;
	}

	public void PauseGame()
	{
		print("PauseMenu: Exec PauseGame()!");
		thePauseMenu.SetActive(true);
		//UIToDeactivate.SetActive(false);
		Time.timeScale = 0f;
		GameIsPaused = true;
		AudioListener.volume = 0f;
	}

	public void Menu()
	{
		print("PauseMenu: Exec Menu()!");
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1f;
		AudioListener.volume = 1f;
	}

	public void QuitGame()
	{
		print("PauseMenu: Exec Quit()!");
		Application.Quit();

	}
}

