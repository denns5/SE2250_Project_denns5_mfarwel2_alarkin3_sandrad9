using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    public Button buttonComponent;

    public float reloadDelay = 1f;// 1 sec delay between rounds

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(OnClick);//adding a listener when the button is made active
    }


    public void OnClick()
    {
        Invoke("ReloadLevel", reloadDelay);//when clicked, invoke the ReloadLevel method with a reload delay
    }

    void ReloadLevel()
    {
        // Reload the scene, resetting the game
        SceneManager.LoadScene("SHUMP Game");
    }

}