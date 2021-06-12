using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Get the Pause Menu
    [SerializeField] private GameObject thisPauseMenu = null;

    private void Update()
    {
        //Press Escape to Open Pause Menu (Only in Gameplay Levels)
        OpenPauseMenu();
    }

    //Start the game
    public void PlayGame()
    {
        SceneManager.LoadScene("Level001");
    }

    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Open Pause Button
    public void OpenPauseMenu()
    {
        //When Press Escape, open Pause Menu
        if (Input.GetKey(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            thisPauseMenu.SetActive(true);
        }
    }

    //Close Button Menu (Continue Button)
    public void ClosePauseMenu()
    {
        thisPauseMenu.SetActive(false);
    }

    //Reset Scene
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Return to Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
