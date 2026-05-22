using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("UI")]
    public TMP_Text healthText; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;
        }
    }

    void Die()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.LoseGame(); // Вызываем экран поражения
        }
    }
}