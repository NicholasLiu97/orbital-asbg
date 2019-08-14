using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsInGame : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Exited the game");
        Application.Quit();
    }
}
