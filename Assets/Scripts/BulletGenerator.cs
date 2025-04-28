using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bullets;
    public float minTime;
    public float maxTime;
    public Vector3 ShootingPosition { get; set; } // Nueva propiedad para la posición de disparo

    void Start()
    {
        ShootingPosition = transform.position; // Inicializar con la posición actual
        Shoot();
    }

    public void ShootInDirection(Vector3 targetPosition)
    {
        GameObject bullet = GameObject.Instantiate(bullets[Random.Range(0, bullets.Length)], transform.position, Quaternion.identity, this.transform);

        // Calcular la dirección hacia el objetivo
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Ajustar la velocidad o dirección de la bala
        GuaguaControler bulletController = bullet.GetComponent<GuaguaControler>();
        if (bulletController != null)
        {
            float bulletSpeed = 15f; // Incrementar la velocidad fija para la bala
            bulletController.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed); // Movimiento en ambas direcciones
        }
    }

    void Shoot()
    {
        // Usar la posición de disparo como objetivo
        ShootInDirection(ShootingPosition);

        Invoke("Shoot", Random.Range(minTime, maxTime));
    }
}
