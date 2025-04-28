using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterBehavior : ObstacleBehavior
{
    private int playerAboveCollisionCount = 0;
    BulletGenerator bulletGenerator;
    private GameObject player;
    private bool isPlayerOnRight;
    private Material originalMaterial;
    private Material redMaterial;

    void Start()
    {
        obstacleType = ObstacleType.Shooter;
        player = GameObject.FindGameObjectWithTag("Player");
        bulletGenerator = GetComponentInChildren<BulletGenerator>();
        if (player != null)
        {
            isPlayerOnRight = player.transform.position.x > transform.position.x;
        }
        originalMaterial = GetComponent<Image>().material;
        redMaterial = new Material(Shader.Find("UI/Default"));
        redMaterial.color = Color.red;
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

            // Actualizar la posición de disparo en el BulletGenerator
            if (bulletGenerator != null)
            {
                bulletGenerator.ShootingPosition = player.transform.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Image>().material = redMaterial; // Apply red shader
            ContactPoint2D[] contactPoints = collision.contacts;
            foreach (var contact in contactPoints)
            {
                // Ajustar la condición para verificar que el punto de contacto esté claramente por encima
                if (contact.point.y > transform.position.y + 0.1f) // Se agrega un margen de 0.1f
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

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Image>().material = originalMaterial; // Revert to original shader
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
