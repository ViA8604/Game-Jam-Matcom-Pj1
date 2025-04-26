using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovePlayer : MonoBehaviour
{
    private Rigidbody2D body;
    private float moveSpeed = 10f;
    private float jumpSpeed = 6f;
    private int jumpCount = 0; // Contar el numero de saltos seguidos

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true; // Para que no se rote
    }

    void Update()
    {
        // Cambia la velocidad del jugador al pulsar las teclas horizontales
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, body.velocity.y);

        // Salto al pulsar espacio, limitado a 2 saltos
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            jumpCount++;
        }

        DontExceedLimits();
    }

    void DontExceedLimits() //Para restringir que el jugador no salga de la pantalla
    {
        // Restringir la posición del jugador dentro de los límites de la pantalla
        Vector3 playerPosition = transform.position;
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect; // Ancho de la mitad de la pantalla
        float screenLeftLimit = Camera.main.transform.position.x - screenHalfWidth;
        float screenRightLimit = Camera.main.transform.position.x + screenHalfWidth;

        // Limitar la posición horizontal del jugador
        playerPosition.x = Mathf.Clamp(playerPosition.x, screenLeftLimit, screenRightLimit);

        // Limitar la posición vertical del jugador (opcional, si es necesario)
        float screenBottomLimit = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float screenTopLimit = Camera.main.transform.position.y + Camera.main.orthographicSize;
        playerPosition.y = Mathf.Clamp(playerPosition.y, screenBottomLimit, screenTopLimit);

        transform.position = playerPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Resetea el contador de saltos al colisionar con el suelo
        if (collision.contacts[0].normal.y > 0.5f) // Check if collision is from below
        {
            jumpCount = 0;
        }
    }
}
