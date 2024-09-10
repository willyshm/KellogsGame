using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeTimer : MonoBehaviour
{
    public float timeLimit = 60f;  // Tiempo límite en segundos
    public TextMeshProUGUI timerText;  // Texto UI para mostrar el temporizador
    public string gameOverScene;  // Escena a cargar si el tiempo se acaba

    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit;  // Inicializa el temporizador con el tiempo límite
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            // Reduce el tiempo restante
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            // Si el tiempo se acaba, cargar la escena de Game Over
            SceneManager.LoadScene(gameOverScene);
        }
    }

    void UpdateTimerUI()
    {
        // Actualiza el texto del temporizador en pantalla
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
