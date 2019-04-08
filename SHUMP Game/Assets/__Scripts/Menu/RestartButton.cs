using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    public Button buttonComponent;//used to make a listener

    // Start is called before the first frame update
    void Start()
    {
        buttonComponent.onClick.AddListener(OnClick);//addding a listener when the button is set to active
    }

    public void OnClick()
    {
        SceneManager.LoadScene("SHUMP Game");//when clicked, Unity will load the Menu scene
    }

}
