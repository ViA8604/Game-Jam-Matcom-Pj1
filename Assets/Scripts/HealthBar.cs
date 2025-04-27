using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private bool smoothTransition = true;
    [SerializeField] private float smoothSpeed = 5f;
    float width;

    private float targetFillAmount = 1f;

    private void Awake()
    {
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }
    void Start()
    {
        width=GetComponent<RectTransform>().rect.width;
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        targetFillAmount = healthPercentage/100;
        
        if (!smoothTransition)
        {
            healthBarFill.GetComponent<RectTransform>().sizeDelta = new Vector2(width*targetFillAmount,GetComponent<RectTransform>().sizeDelta.y);
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