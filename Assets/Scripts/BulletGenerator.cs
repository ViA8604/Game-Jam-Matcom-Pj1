using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bullets;
    public float minTime;
    public float maxTime;
    void Start()
    {
        Shoot();
    }
    void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bullets[Random.Range(0, bullets.Length)], transform.position, Quaternion.identity, this.transform);

        // Determine shooting direction based on the scale of the parent object
        Vector3 parentScale = transform.parent != null ? transform.parent.localScale : Vector3.one;
        float direction = parentScale.x > 0 ? -1 : 1; // Invert the logic: Positive for left, negative for right

        // Adjust bullet's velocity or direction
        GuaguaControler bulletController = bullet.GetComponent<GuaguaControler>();
        if (bulletController != null)
        {
            float bulletSpeed = 5f; // Set a fixed speed for the bullet
            bulletController.velocity = new Vector2(direction * bulletSpeed, 0); // Ensure movement is only on the X-axis
        }

        Invoke("Shoot", Random.Range(minTime, maxTime));
    }
}
