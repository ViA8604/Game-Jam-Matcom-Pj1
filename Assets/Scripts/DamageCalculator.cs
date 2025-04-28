using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public float minDamage = 5f;
    public float maxDamage = 50f;
    public float minImpactForce = 2f; // Fuerza mínima para causar daño
    public float maxImpactForce = 20f; // Fuerza a partir de la cual se aplica daño máximo

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle")) return;

        float impactForce = collision.relativeVelocity.magnitude;
        float damage = CalculateDamage(impactForce);

        Debug.Log($"Impacto con fuerza: {impactForce}. Daño recibido: {damage}");

        ApplyDamage(damage);
        HandleObstacleBehavior(collision.gameObject);
    }

    private void ApplyDamage(float damage)
    {
        GetComponent<HealthSystem>().TakeDamage(damage);
    }

    private void HandleObstacleBehavior(GameObject obstacle)
    {
        var obstacleBehavior = obstacle.GetComponent<ObstacleBehavior>();
        if (obstacleBehavior == null) return;

        if (obstacleBehavior.obstacleType == ObstacleBehavior.ObstacleType.Mobile)
        {
            Destroy(obstacle); //  destruye GameObject completo
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
