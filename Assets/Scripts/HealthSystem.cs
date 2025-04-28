using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private float damageMultiplier = 0.5f; // Reduce damage by 50%
    [SerializeField] private float immunityDuration = 2f; // Duration of immunity in seconds
    private bool isImmune = false;
    private float immunityTimer = 0f;

    public event System.Action OnDeath;
    public event System.Action<float> OnHealthChanged; // Evento para cambios de salud

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;
            if (immunityTimer <= 0f)
            {
                isImmune = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isImmune) return; // Ignore damage if immune

        currentHealth = Mathf.Clamp(currentHealth - (damage * damageMultiplier), 0f, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth); // Notificar cambio (0-1)

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            isImmune = true;
            immunityTimer = immunityDuration; // Reset immunity timer
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
