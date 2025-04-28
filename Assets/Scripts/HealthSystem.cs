using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth ;

    public event System.Action OnDeath;
    public event System.Action<float> OnHealthChanged; // Evento para cambios de salud

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth); // Notificar cambio (0-1)
        
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log(gameObject.name + " ha muerto");
        GameObject storyObject = new GameObject("Lose");
        storyObject.tag = "info";
        DontDestroyOnLoad(storyObject);
        SceneManager.LoadScene("StoryDashboardScene");
    }
}
