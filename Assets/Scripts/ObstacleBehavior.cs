using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public ObstacleType obstacleType;


    [SerializeField]
    public enum ObstacleType 
    {
        Mobile,
        Stable,
        Shooter
    };
}
