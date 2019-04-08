using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Used when play button is pressed on main menu to switch to SHUMP scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Used to quit the game on main menu
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
