using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public GameObject menuPanelPrefabInstance; // Referencia al prefab MenuPanel

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void Options()
    {
        if (menuPanelPrefabInstance == null)
        {
            GameObject menuPanelPrefab = Resources.Load<GameObject>("Prefabs/MenuPanel"); // Carga el prefab desde la carpeta Resources
            menuPanelPrefabInstance = Instantiate(menuPanelPrefab, GameObject.Find("Canvas").transform); // Instancia el prefab como hijo del canvas para que se muestre
        }
        menuPanelPrefabInstance.SetActive(true); // Activa el panel de opciones
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
