using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : ObstacleBehavior
{
    private int playerAboveCollisionCount = 0;
    private GameObject player;
    private bool isPlayerOnRight;

    void Start()
    {
        obstacleType = ObstacleType.Shooter;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            isPlayerOnRight = player.transform.position.x > transform.position.x;
        }
    }

    void Update()
    {
        if (player != null)
        {
            bool currentPlayerOnRight = player.transform.position.x > transform.position.x;
            if (currentPlayerOnRight != isPlayerOnRight)
            {
                isPlayerOnRight = currentPlayerOnRight;
                RotateGameObject();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D[] contactPoints = collision.contacts;
            foreach (var contact in contactPoints)
            {
                if (contact.point.y > transform.position.y)
                {
                    playerAboveCollisionCount++;
                    Debug.Log($"Player collided from above! Count: {playerAboveCollisionCount}");
                    if (playerAboveCollisionCount >= 2)
                    {
                        Destroy(gameObject);
                    }
                    break;
                }
            }
        }
    }

    private void RotateGameObject()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip the GameObject horizontally
        transform.localScale = localScale;
        Debug.Log("GameObject mirrored due to player side change.");
    }
}
