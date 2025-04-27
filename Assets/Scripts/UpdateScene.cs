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
        UpdateObstacleArea();

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

    void UpdateObstacleArea()
    {
        List<GameObject> obstacles = LoadObstacles();
        ClearOldObstacles();
        SpawnNewObstacles(obstacles);
    }

    List<GameObject> LoadObstacles()
    {
        List<GameObject> obstacles = new List<GameObject>();
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        foreach (GameObject prefab in loadedPrefabs)
        {
            // Ignora los prefabs si su nombre es "and" o "or"
            if (prefab.CompareTag("Obstacle") && prefab.name != "and" && prefab.name != "or")
            {
                obstacles.Add(prefab);
            }
        }
        return obstacles;
    }

    void ClearOldObstacles()
    {
        if (obstaclesArea.transform.childCount != 0)
        {
            // Elimina los obstáculos viejos
            foreach (Transform child in obstaclesArea.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void SpawnNewObstacles(List<GameObject> obstacles)
    {
        Vector3 areaScale = obstaclesArea.transform.localScale;

        // Carga 4 obstáculos nuevos seleccionados aleatoriamente
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, obstacles.Count);
            GameObject newObstacle = Instantiate(obstacles[randomIndex], obstaclesArea.transform);

            // Genera una posición aleatoria dentro del área
            float randomX = Random.Range(-areaScale.x / 2, areaScale.x / 2);
            float randomZ = Random.Range(-areaScale.z / 2, areaScale.z / 2);
            newObstacle.transform.localPosition = new Vector3(randomX, 0, randomZ);
        }
    }
}
