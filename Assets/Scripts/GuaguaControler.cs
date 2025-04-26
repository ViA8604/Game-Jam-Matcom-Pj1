using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuaguaControler : MonoBehaviour
{
    

    void FixedUpdate(){
        GetComponent<Rigidbody2D>().velocity=new Vector2(-3,0);
    }
}
