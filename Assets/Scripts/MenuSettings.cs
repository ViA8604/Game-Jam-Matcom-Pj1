using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TMPro;

public class MenuSettings : MonoBehaviour
{
    public Toggle fullscreen;
    public ResolutionsDropdown resolutionDropdownScript;
    
    public Button confirmButton;
    public Button cancelButton;

    void Start()
    {
        fullscreen = gameObject.transform.GetComponentInChildren<Toggle>();
        resolutionDropdownScript = gameObject.transform.GetComponentInChildren<ResolutionsDropdown>();

        if (fullscreen != null)
        {
            fullscreen.onValueChanged.AddListener(SetFullscreen); // Agrega el listener
        }

        confirmButton = transform.Find("ConfirmChngButton").GetComponent<Button>(); // Busca entre los hijos
        cancelButton = transform.Find("ResetChngButton").GetComponent<Button>(); // Busca entre los hijos

        confirmButton.onClick.AddListener(ConfirmChanges); // Asigna el evento al botón Confirm
        cancelButton.onClick.AddListener(CancelChanges); // Asigna el evento al botón Cancel
    }

    void Update()
    {
        
    }

    void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; // Cambia entre ventana y pantalla completa
    }

    void ConfirmChanges()
    {
        gameObject.SetActive(false); // Desactiva el GameObject actual
        // Los cambios ya están aplicados, no se necesita más acción
    }

    void CancelChanges()
    {
        if (resolutionDropdownScript.HasResolutionChanged)
        {
            resolutionDropdownScript.RevertResolution(); // Restaura la resolución anterior
        }
        gameObject.SetActive(false); // Desactiva el GameObject actual
    }
}
