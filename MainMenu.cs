using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //changes scene to default scene onclick
    public void playGame()
    {
        SceneManager.LoadScene("Normal");
    }

    //changes scene to easy scene onclick
    public void EasyMode()
    {
        SceneManager.LoadScene("Easy");
    }

    //changes scene to easy scene onclick
    public void HardMode()
    {
        SceneManager.LoadScene("Hard");
    }

    //changes scene to rules scene onclick
    public void RulesPage()
    {
        SceneManager.LoadScene("RulesPage");
    }

    //changes scene to select game modes scene onclick
    public void GameModes()
    {
        SceneManager.LoadScene("GameModes");
    }

    //returns to mainmenu onclick
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");    
    }

    //quits application
    public void QuitGame()
    {
        Application.Quit();
    }
}

