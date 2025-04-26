using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    GameObject menuPanelPrefabInstance; // Referencia al prefab MenuPanel
    GameObject infoPanelPrefabInstance; // Referencia al prefab InfoPanel
    GameObject pausePanelPrefabInstance; // Referencia al prefab PausePanel

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

    public void ShowThanksInfo()
    {
        if(infoPanelPrefabInstance == null)
        {
            GameObject infoPanelPrefab = Resources.Load<GameObject>("Prefabs/InfoPanel"); // Carga el prefab desde la carpeta Resources
            infoPanelPrefabInstance = Instantiate(infoPanelPrefab, GameObject.Find("Canvas").transform); // Instancia el prefab como hijo del canvas para que se muestre

            Button thanksButton = infoPanelPrefabInstance.GetComponentInChildren<Button>();
            thanksButton.onClick.AddListener(() => thanksButton.transform.parent.gameObject.SetActive(false));
        }
        else
        {
        infoPanelPrefabInstance.SetActive(true);
        }
    }
    
    public void PauseGame()
    {
        if(infoPanelPrefabInstance == null)
        {
            GameObject pausePanelPrefab = Resources.Load<GameObject>("Prefabs/PauseMenu"); // Carga el prefab desde la carpeta Resources
            pausePanelPrefabInstance = Instantiate(pausePanelPrefab, GameObject.Find("Canvas").transform); // Instancia el prefab como hijo del canvas para que se muestre
        
            Button resumeButton = pausePanelPrefabInstance.transform.Find("ResumeButton").GetComponent<Button>();
            resumeButton.onClick.AddListener(() => pausePanelPrefabInstance.SetActive(false));

            Button returnToM = pausePanelPrefabInstance.transform.Find("ReturnMButton").GetComponent<Button>();
            returnToM.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

            Button exitGButton = pausePanelPrefabInstance.transform.Find("ExitGButton").GetComponent<Button>();
            exitGButton.onClick.AddListener(ExitGame);
        }

        else
        {
        infoPanelPrefabInstance.SetActive(true);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
