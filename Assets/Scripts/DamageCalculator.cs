using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public float minDamage = 5f;
    public float maxDamage = 50f;
    public float minImpactForce = 2f; // Fuerza mínima para causar daño
    public float maxImpactForce = 20f; // Fuerza a partir de la cual se aplica daño máximo

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("colision");
        if (collider.CompareTag("Obstacle"))
        {
            // Calcular la fuerza relativa del impacto
            //float impactForce = collision.relativeVelocity.magnitude;
            
            // Calcular el daño proporcional a la fuerza
            float damage = 15;
            
            //Debug.Log($"Impacto con fuerza: {impactForce}. Daño recibido: {damage}");
            
            // Aquí aplicas el daño a tu sistema de salud
            GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }

    private float CalculateDamage(float impactForce)
    {
        // Si la fuerza es menor que el mínimo, no causa daño
        if (impactForce < minImpactForce) return 0f;
        
        // Normalizar la fuerza entre los valores mínimo y máximo
        float normalizedForce = Mathf.InverseLerp(minImpactForce, maxImpactForce, impactForce);
        
        // Asegurarse de que no exceda el máximo
        normalizedForce = Mathf.Clamp01(normalizedForce);
        
        // Calcular daño proporcional
        return Mathf.Lerp(minDamage, maxDamage, normalizedForce);
    }
}
