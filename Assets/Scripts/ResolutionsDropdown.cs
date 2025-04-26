using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class ResolutionsDropdown : MonoBehaviour
{
    TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public bool HasResolutionChanged { get; private set; } = false; // Nueva propiedad
    Resolution previousResolution; // Almacena la resolución anterior

    // Start is called before the first frame update
    void Start()
    {
        previousResolution = Screen.currentResolution; // Guarda la resolución inicial
        GetDropdownComponent();
        PopulateResolutionOptions();
        SetCurrentResolutionLabel(); // Establece el label con la resolución actual
        resolutionDropdown.onValueChanged.AddListener(SetResolution); // Agrega el listener
    }

    void GetDropdownComponent()
    {
        resolutionDropdown = GetComponent<TMP_Dropdown>(); // Dame el TMP_Dropdown
        if (resolutionDropdown == null)
        {
            Debug.LogError("TMP_Dropdown component not found! Make sure the script is attached to the GameObject with the TMP_Dropdown.");
            return; // Evita continuar si no se encuentra el componente
        }

        if (resolutionDropdown.template == null)
        {
            Debug.LogError("TMP_Dropdown Template is not assigned! Make sure the Template is set in the TMP_Dropdown component.");
            return;
        }
    }

    void PopulateResolutionOptions()
    {
        resolutions = Screen.resolutions.Select(res => new Resolution { width = res.width, height = res.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = resolutions.Select(ResolutionToString).ToList(); // Usa el nuevo método
        resolutionDropdown.AddOptions(options);
    }

    void SetCurrentResolutionLabel()
    {
        Resolution currentResolution = Screen.currentResolution;
        string currentResolutionText = ResolutionToString(currentResolution);

        int currentIndex = resolutions.ToList().FindIndex(res => res.width == currentResolution.width && res.height == currentResolution.height);
        if (currentIndex != -1)
        {
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.captionText.text = currentResolutionText;
        }
    }

    string ResolutionToString(Resolution resolution)
    {
        return resolution.width + " x " + resolution.height; // Convierte una resolución a texto
    }

    void SetResolution(int resolutionIndex)
    {
        previousResolution = Screen.currentResolution; // Actualiza la resolución anterior
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen); // Cambia la resolución
        HasResolutionChanged = true; // Marca que hubo un cambio
    }

    public void RevertResolution()
    {
        Screen.SetResolution(previousResolution.width, previousResolution.height, Screen.fullScreen); // Restaura la resolución anterior
        HasResolutionChanged = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
