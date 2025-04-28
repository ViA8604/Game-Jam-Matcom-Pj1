using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuButtons : MonoBehaviour
{
    GameObject menuPanelPrefabInstance; // Referencia al prefab MenuPanel
    GameObject infoPanelPrefabInstance; // Referencia al prefab InfoPanel
    GameObject pausePanelPrefabInstance; // Referencia al prefab PausePanel

    bool showedStory = false; // Variable para controlar si la historia ya se mostró

    void Start()
    {
        // Initialize ShowedStory to 0 if it doesn't exist
        if (!PlayerPrefs.HasKey("ShowedStory"))
        {
            PlayerPrefs.SetInt("ShowedStory", 0);
            PlayerPrefs.Save(); // Ensure the value is saved to disk
            Debug.Log("Initialized ShowedStory to 0");
        }

        // Apply global audio preference
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            bool isAudioOn = PlayerPrefs.GetInt("AudioEnabled", 1) == 1;
            audio.mute = !isAudioOn;
        }
    }

    public void PlayGame()
    {
        Debug.Log($"Current Scene: {SceneManager.GetActiveScene().name}, ShowedStory: {PlayerPrefs.GetInt("ShowedStory", 0)}");

        if (SceneManager.GetActiveScene().name == "MainScene" && PlayerPrefs.GetInt("ShowedStory", 0) == 0)
        {   
            Debug.Log("Loading StoryDashboardScene");
            GameObject storyObject = new GameObject("story");
            storyObject.tag = "info";
            DontDestroyOnLoad(storyObject); // Ensure the object persists across scenes
            SceneManager.LoadScene("StoryDashboardScene");
        }
        else
        {
            Debug.Log("Loading GameScene");
            SceneManager.LoadScene("GameScene");
        }
    }
    
    public void Options()
    {
        if (menuPanelPrefabInstance == null)
        {   
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            GameObject menuPanelPrefab = Resources.Load<GameObject>("Prefabs/MenuPanel"); // Carga el prefab desde la carpeta Resources
            menuPanelPrefabInstance = Instantiate(menuPanelPrefab, GameObject.Find("Canvas").transform); // Instancia el prefab como hijo del canvas para que se muestre
            menuPanelPrefabInstance.GetComponent<MenuSettings>().music = audio;
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

    void OnApplicationQuit()
    {
        ResetShowedStory();
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            ResetShowedStory();
        }
    }
#endif

    private void ResetShowedStory()
    {
        PlayerPrefs.SetInt("ShowedStory", 0); // Reset ShowedStory to 0
        PlayerPrefs.Save(); // Ensure the value is saved to disk
        Debug.Log("Reset ShowedStory to 0");
    }
}
