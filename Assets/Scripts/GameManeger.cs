using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int collectedItems = 0;
    public int totalItemsRequired = 10;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CollectItem()
    {
        collectedItems++;
        
        // Verificar si se recolectaron todos los ítems
        if (collectedItems >= totalItemsRequired)
        {
            WinGame();
        }
        
        Debug.Log($"Ítems recolectados: {collectedItems}/{totalItemsRequired}");
    }
    
    private void WinGame()
    {
        Debug.Log("¡Has ganado! Recolectaste todos los ítems.");
        // Aquí puedes cargar una escena de victoria o mostrar un UI
        GameObject storyObject = new GameObject("Win");
        storyObject.tag = "info";
        DontDestroyOnLoad(storyObject);
        SceneManager.LoadScene("StoryDashboardScene");
    }
    
    public void ResetGame()
    {
        collectedItems = 0;
    }
}