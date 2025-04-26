using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateScene : MonoBehaviour
{
    Canvas canvas;
    public Image backgroundImg;
    GameObject obstaclesArea;   //En un futuro apareceran aqui random
    // Start is called before the first frame update
    void Start()
    {
        //Inicializa la escena
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        backgroundImg = canvas.GetComponentInChildren<Image>();
        obstaclesArea = GameObject.FindGameObjectWithTag("ObstaclesArea");
        LoadNewerScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewerScene()
    {
        UpddateBackground();

    }

    void UpddateBackground()
    {
        if (backgroundImg.sprite == null)   //Carga el fondo la 1ra vez
        {
            backgroundImg.sprite = Resources.Load<Sprite>("Img/backgrounds/1");
            Debug.Log("Background loaded");
        }
        
        else    //Actualiza el fondo random
        {
            int imgNumber = Random.Range(1, 4);
            backgroundImg.sprite = Resources.Load<Sprite>("Img/backgrounds/" + imgNumber);
        }
        
    }
}
