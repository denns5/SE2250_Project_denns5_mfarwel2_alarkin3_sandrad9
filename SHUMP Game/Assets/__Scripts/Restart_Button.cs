using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour
{

    public Button buttonComponent;

    public float reloadDelay = 2f;// 2 sec delay between rounds

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        Invoke("ReloadLevel", reloadDelay);
    }

    void ReloadLevel()
    {
        // Reload the scene, resetting the game
        SceneManager.LoadScene("__Prospector_Scene_0");
    }

}