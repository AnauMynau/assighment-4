using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public TMP_Text scoreText;
    private int score = 0;
    public int scoreToWin = 10; // Сколько фрагов нужно для победы

    [Header("UI Конца игры")]
    public GameObject gameOverPanel;
    public TMP_Text resultText;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false); 
        
        Time.timeScale = 1f; // Нормальное время
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "KILLS: " + score;
    }

    public void LoseGame()
    {
        ShowGameOverScreen("YOU DIED", Color.red);
    }

    public void WinGame()
    {
        ShowGameOverScreen("YOU WIN!", Color.green);
    }

    void ShowGameOverScreen(string message, Color textColor)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            resultText.text = message;
            resultText.color = textColor;
            
            Time.timeScale = 0f; // Ставим игру на паузу
            
            // Открываем курсор, чтобы нажать кнопку
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Этот метод привяжем к кнопке
    public void RestartGame()
    {
        Time.timeScale = 1f; // Возвращаем время
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}