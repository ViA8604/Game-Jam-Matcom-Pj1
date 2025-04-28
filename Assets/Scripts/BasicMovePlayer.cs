using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovePlayer : MonoBehaviour
{
    private Rigidbody2D body;
    private float moveSpeed = 8f;
    private float jumpSpeed = 6f;
    private int jumpCount = 0; // Contar el numero de saltos seguidos
    private bool hasHandledRightEdge = false; // Nueva bandera

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
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            body.constraints=RigidbodyConstraints2D.None;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            jumpCount++;
            Invoke("Freezear",1.2f);
        }

        DontExceedLimits();
    }
    void Freezear(){
        body.constraints=RigidbodyConstraints2D.FreezePositionY;
        jumpCount=0;
    }

    void DontExceedLimits() //Para restringir que el jugador no salga de la pantalla
    {
        Vector3 playerPosition = transform.position;
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect; // Ancho de la mitad de la pantalla
        float screenLeftLimit = Camera.main.transform.position.x - screenHalfWidth;
        float screenRightLimit = Camera.main.transform.position.x + screenHalfWidth;

        // Limitar la posición horizontal del jugador al borde izquierdo
        playerPosition.x = Mathf.Clamp(playerPosition.x, screenLeftLimit, screenRightLimit);

        // Llamar a HandleRightScreenEdge si el jugador está en el borde derecho y no se ha manejado aún
        if (playerPosition.x >= screenRightLimit && !hasHandledRightEdge)
        {
            HandleRightScreenEdge(ref playerPosition); // Pasar por referencia para que no de bateo
            hasHandledRightEdge = true; // Marcar como manejado
        }

        // Restablecer la bandera si el jugador ya no está en el borde derecho
        if (playerPosition.x < screenRightLimit)
        {
            hasHandledRightEdge = false;
        }

        // Limitar la posición vertical del jugador (opcional, si es necesario)
        float screenBottomLimit = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float screenTopLimit = Camera.main.transform.position.y + Camera.main.orthographicSize;
        playerPosition.y = Mathf.Clamp(playerPosition.y, screenBottomLimit, screenTopLimit);

        transform.position = playerPosition;
    }

    void HandleRightScreenEdge(ref Vector3 playerPosition)
    {   
        //Actualiza la escena y coloca al jugador al inicio
        GameObject.FindWithTag("SceneManager").GetComponent<UpdateScene>().LoadNewerScene();
        playerPosition = new Vector3(-8.27f, -2.76f, 0f);
        transform.position = playerPosition; // Actualiza la posición del jugador
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
