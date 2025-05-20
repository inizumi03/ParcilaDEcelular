using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;         // Texto en tiempo real
    public Text finalScoreText;    // Texto que aparece al final

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persistencia (opcional)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + score.ToString();
    }

    public void ShowFinalScore()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Puntaje Final: " + score.ToString();
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;  // Despausar el juego
        score = 0;            // Reiniciar puntaje
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
