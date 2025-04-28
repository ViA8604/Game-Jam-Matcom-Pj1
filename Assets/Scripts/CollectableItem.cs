using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notificar al GameManager que se recolectó un ítem
            GameManager.instance.CollectItem();
            Destroy(gameObject);
        }
    }
}
