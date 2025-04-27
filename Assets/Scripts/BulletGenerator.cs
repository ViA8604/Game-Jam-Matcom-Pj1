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
    void Shoot(){
        GameObject.Instantiate(bullets[Random.Range(0,bullets.Length)],transform.position,Quaternion.identity);
        Invoke("Shoot",Random.Range(minTime,maxTime));
    }
}
