using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private bool smoothTransition = true;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetFillAmount = 1f;

    private void Awake()
    {
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        targetFillAmount = healthPercentage;
        
        if (!smoothTransition)
        {
            healthBarFill.fillAmount = targetFillAmount;
        }
    }

    private void Update()
    {
        if (smoothTransition)
        {
            healthBarFill.fillAmount = Mathf.Lerp(
                healthBarFill.fillAmount, 
                targetFillAmount, 
                smoothSpeed * Time.deltaTime
            );
        }
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthChanged -= UpdateHealthBar;
    }
}