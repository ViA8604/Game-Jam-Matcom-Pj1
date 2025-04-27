using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadHorizontal = 2f;  // Velocidad hacia la izquierda
    public float amplitudOnda = 1f;         // Altura de la onda
    public float frecuenciaOnda = 1f;       // Frecuencia de oscilación
    
    private Vector3 posicionInicial;
    private float tiempoInicio;

    void Start()
    {
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
    }

    void Update()
    {
        // Calculamos el desplazamiento horizontal (hacia la izquierda en eje X)
        float desplazamientoX = posicionInicial.x - (Time.time - tiempoInicio) * velocidadHorizontal;
        
        // Calculamos el movimiento ondulatorio en el eje Y
        float ondaY = Mathf.Sin((Time.time - tiempoInicio) * frecuenciaOnda) * amplitudOnda;
        
        // Aplicamos la nueva posición
        transform.position = new Vector3(desplazamientoX, posicionInicial.y + ondaY, posicionInicial.z);
    }
}


