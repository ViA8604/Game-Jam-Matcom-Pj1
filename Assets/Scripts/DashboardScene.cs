using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DashboardScene : MenuButtons
{
    GameObject info;
    
    Button Go;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("Info");
        Go = GameObject.Find("Go").GetComponent<Button>();
        SetButtonAction();
    }

    private void SetButtonAction()
    {
        if(info != null)
        {
            if(info.name == "Win")
                {
                    Go.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
                }
            else if (info.name == "Lose")
                {
                    Go.onClick.AddListener(() => PlayGame());
                }
            else
                {
                    //Contar historia
                    Go.onClick.AddListener(() => PlayGame());
                }

        }
    }
}
