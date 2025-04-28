using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public ObstacleType obstacleType;

    public enum ObstacleType 
    {
        Mobile,
        Stable,
        Shooter
    };

    void Update()
    {
        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
