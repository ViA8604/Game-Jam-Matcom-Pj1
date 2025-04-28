using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UpdateScene : MonoBehaviour
{
    Canvas canvas;
    public Image backgroundImg;
    GameObject obstaclesArea;
    Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>> obstaclesTypes;
    Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>> prefabObstacles; // Cache de prefabs
    Sprite[] backgroundSprites; // Cache de fondos

    List<GameObject> platformsPositions;
    List<GameObject> instantiatedRewards;

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        backgroundImg = canvas.GetComponentInChildren<Image>();
        obstaclesArea = GameObject.FindGameObjectWithTag("ObstaclesArea");
        obstaclesTypes = new Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>>();
        prefabObstacles = LoadObstacles(); // Carga los prefabs una vez
        backgroundSprites = Resources.LoadAll<Sprite>("Img/backgrounds"); // Carga los fondos una vez
        platformsPositions = new List<GameObject>();

        foreach (Transform child in obstaclesArea.transform)
        {
            ObstacleBehavior obstacleBehavior = child.GetComponent<ObstacleBehavior>();
            if (obstacleBehavior != null)
            {
                ObstacleBehavior.ObstacleType type = obstacleBehavior.obstacleType;
                if (!obstaclesTypes.ContainsKey(type))
                {
                    obstaclesTypes[type] = new List<GameObject>();
                }
                obstaclesTypes[type].Add(child.gameObject);
            }
        }
        GameObject platformContainer = GameObject.FindGameObjectWithTag("platformContainer");
        foreach (Transform child in platformContainer.transform)
        {
                platformsPositions.Add(child.gameObject);
        }
        LoadNewerScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewerScene()
    {
        UpdateBackground();
        UpdateObstacleArea();
    }

    void UpdateBackground()
    {
        if (backgroundImg.sprite == null)
        {
            backgroundImg.sprite = backgroundSprites.FirstOrDefault(s => s.name == "0");
            Debug.Log("Background loaded");
        }
        else
        {
            int currentImgNumber;
            if (!int.TryParse(backgroundImg.sprite.name, out currentImgNumber))
            {
                Debug.LogError($"Invalid sprite name format: {backgroundImg.sprite.name}");
                return; // Exit the method if parsing fails
            }

            Sprite newSprite;
            do
            {
                newSprite = backgroundSprites[Random.Range(2, 7)];
            } while (int.TryParse(newSprite.name, out int newImgNumber) && newImgNumber == currentImgNumber);

            backgroundImg.sprite = newSprite;
        }
    }

    void UpdateObstacleArea()
    {
        ClearOldObstacles();
        SpawnNewObstacles();
        ClearOldRewards();
        UpdateRewards();
    }

    void ClearOldObstacles()
    {
        foreach (Transform child in obstaclesArea.transform)
        {
            foreach (Transform innerChild in child)
            {
                Destroy(innerChild.gameObject);
            }
        }
    }

    void ClearOldRewards()
    {
        if(instantiatedRewards == null)
            return;
        foreach (GameObject reward in instantiatedRewards)
        {
            Destroy(reward);
        }
        instantiatedRewards.Clear();
    }
    
    void SpawnNewObstacles()
    {
        foreach (var obstacleTypeEntry in obstaclesTypes)
        {
            ObstacleBehavior.ObstacleType type = obstacleTypeEntry.Key;
            List<GameObject> typePrefabs = prefabObstacles.ContainsKey(type) ? prefabObstacles[type] : null;

            if (typePrefabs == null || typePrefabs.Count == 0)
                continue;

            GameObject parentObject = obstacleTypeEntry.Value.FirstOrDefault();
            if (parentObject == null)
                continue;

            Vector3 parentScale = parentObject.transform.localScale;
            HashSet<Vector3> usedPositions = new HashSet<Vector3>(); // Track used positions

            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0, typePrefabs.Count);
                GameObject newObstacle = Instantiate(typePrefabs[randomIndex], parentObject.transform);

                Vector3 newPosition;
                do
                {
                    float randomOffsetX = Random.Range(-0.3f, 0.3f);
                    float randomOffsetZ = Random.Range(-0.3f, 0.3f);

                    newPosition = new Vector3(
                        Random.Range(-parentScale.x / 2, parentScale.x / 2) + randomOffsetX,
                        0,
                        Random.Range(-parentScale.z / 2, parentScale.z / 2) + randomOffsetZ
                    );
                } while (usedPositions.Contains(newPosition)); // Ensure position is unique

                usedPositions.Add(newPosition); // Mark position as used
                newObstacle.transform.localPosition = newPosition;
            }
        }
    }

    void UpdateRewards()
    {
        GameObject rewardPrefab = Resources.Load<GameObject>("Prefabs/Reward");
        if (rewardPrefab == null)
        {
            Debug.LogError("Reward prefab not found in Resources.");
            return;
        }

        instantiatedRewards = new List<GameObject>(); // Ensure the list is initialized

        int rewardsToInstantiate = Random.Range(2, 6); // Randomize number of rewards (2 to 5)
        List<GameObject> shuffledPositions = platformsPositions.OrderBy(x => Random.value).ToList(); // Shuffle positions

        for (int i = 0; i < rewardsToInstantiate && i < shuffledPositions.Count; i++)
        {
            GameObject position = shuffledPositions[i];
            GameObject newReward = Instantiate(rewardPrefab, position.transform);
            newReward.transform.localPosition = new Vector3(0, 120f, 0); // Position slightly above the parent
            newReward.transform.localScale = Vector3.one * 1f; // Set scale to 0.8
            instantiatedRewards.Add(newReward); // Add to the list
        }
    }

    Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>> LoadObstacles()
    {
        Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>> obstacles = new Dictionary<ObstacleBehavior.ObstacleType, List<GameObject>>();
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("Prefabs");

        foreach (GameObject prefab in loadedPrefabs)
        {
            if (prefab.CompareTag("Obstacle") && prefab.name != "and" && prefab.name != "or")
            {
                ObstacleBehavior obstacleBehavior = prefab.GetComponent<ObstacleBehavior>();
                if (obstacleBehavior != null)
                {
                    ObstacleBehavior.ObstacleType type = obstacleBehavior.obstacleType;
                    if (!obstacles.ContainsKey(type))
                    {
                        obstacles[type] = new List<GameObject>();
                    }
                    obstacles[type].Add(prefab);
                }
            }
        }
        return obstacles;
    }
}
