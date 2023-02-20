using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerInput player;
    private float timeRemaining;

    public int points;
    bool gameOver = false;
    void Start()
    {
        timeRemaining = MenuManager.instance.tempoDoJogo;
        pointText.text = points.ToString();
        gameOverPanel.SetActive(false);
        
        GameObject obj = GameObject.Find("MenuManager");

        if (obj != null)
        {
            Destroy(obj);
        }
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        // Verifica se o tempo restante é menor ou igual a 0
        if (timeRemaining <= 0)
        {
            GameOver();
            return;
        }

        // Converte o tempo restante em minutos e segundos
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Atualiza o texto mostrando o tempo restante
        if (countdownText != null && !gameOver)
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        pointText.text = points.ToString();
        if (player == null)
        {
            GameOver();
        }
    }
    public void SceneMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ReestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void GameOver()
    {
        player.isGameOver = true;
        gameOver = true;
        gameOverPanel.SetActive(true);
    }
    
}
