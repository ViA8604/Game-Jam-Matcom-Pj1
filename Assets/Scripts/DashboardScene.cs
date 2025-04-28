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

    Image backImage;
    private int imageIndex = 0; // Track the current image index
    private List<string> storyImages = new List<string> { "Img/story/story1", "Img/story/story2" }; // List of story images

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.FindGameObjectWithTag("info");
        Go = GameObject.Find("GoButton").GetComponent<Button>();
        backImage = GameObject.Find("backImage").GetComponent<Image>();

        if (info != null && info.name != "Win" && info.name != "Lose")
        {
            ChangeImage("Img/story/story1"); // Show the first story image immediately
            imageIndex++; // Increment the index to track the first image
        }

        SetButtonAction();
    }

    private void SetButtonAction()
    {
        if (info != null)
        {
            if (info.name == "Win")
            {
                ChangeImage("Img/story/win"); // Show the win image
                Destroy(info);
                Go.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
            }
            else if (info.name == "Lose")
            {
                ChangeImage("Img/story/gameOver"); // Show the lose image
                Destroy(info);
                Go.onClick.AddListener(() => SceneManager.LoadScene("MainScene")); // Go back to the main menu
            }
            else
            {
                Go.onClick.AddListener(() =>
                {
                    if (imageIndex < storyImages.Count)
                    {
                        ChangeImage(storyImages[imageIndex]);
                        imageIndex++;
                    }
                    else
                    {
                        Debug.Log("Marking ShowedStory as 1 and loading GameScene");
                        PlayerPrefs.SetInt("ShowedStory", 1); // Mark story as shown
                        PlayerPrefs.Save(); // Ensure the value is saved
                        Destroy(info); // Destroy the info object
                        SceneManager.LoadScene("GameScene");
                    }
                });
            }
        }
    }

    private void ChangeImage(string route)
    {
        backImage.sprite = Resources.Load<Sprite>(route);
    }
}
