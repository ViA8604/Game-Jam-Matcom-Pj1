using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewadsControler : MonoBehaviour
{
    public Text itemsText;
    
    void Update()
    {
        itemsText.text = $"Ítems {GameManager.instance.collectedItems}/{GameManager.instance.totalItemsRequired}";
    }
}
