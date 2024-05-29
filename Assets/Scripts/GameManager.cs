using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool isGameActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartGame(); // Start the game automatically when the scene is loaded
    }

    void Update()
    {
        if (isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver();
            }
            UpdateTimerText();
        }
    }

    void StartGame()
    {
        isGameActive = true;
        timeRemaining = gameDuration;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Round(timeRemaining);
    }

    void GameOver()
    {
        isGameActive = false;
        PlayerPrefs.SetInt("FinalScore", score);  // Store the score
        SceneManager.LoadScene("GameOverScene");
    }
}