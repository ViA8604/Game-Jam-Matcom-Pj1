using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuaguaControler : MonoBehaviour
{
    public Vector2 velocity = new Vector2(-3, 0); // Default velocity

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
