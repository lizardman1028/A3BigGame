using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    [Header("Game State Settings")]
    [SerializeField] private float winConditionClicks = 1000000f; // 1 million clicks to "graduate"
    [SerializeField] private float gameTimeLimit = 600f; // 10 minutes time limit (optional)
    
    [Header("UI References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI timeRemainingText;
    
    private bool gameEnded = false;
    private float gameStartTime;
    private bool useTimeLimit = false; // Set to true if you want a time-based challenge
    
    public static GameStateManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameStartTime = Time.time;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        if (gameEnded) return;
        
        CheckWinCondition();
        
        if (useTimeLimit)
        {
            CheckTimeLimit();
            UpdateTimeDisplay();
        }
    }
    
    private void CheckWinCondition()
    {
        if (GlobalInjector.inventoryManager == null) return;
        
        if (GlobalInjector.inventoryManager.Clicks >= winConditionClicks)
        {
            TriggerWin();
        }
    }
    
    private void CheckTimeLimit()
    {
        if (Time.time - gameStartTime >= gameTimeLimit)
        {
            TriggerGameOver("Time's up! You ran out of time to graduate from Stanford!");
        }
    }
    
    private void UpdateTimeDisplay()
    {
        if (timeRemainingText != null)
        {
            float timeRemaining = gameTimeLimit - (Time.time - gameStartTime);
            if (timeRemaining > 0)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                timeRemainingText.text = $"Time: {minutes:00}:{seconds:00}";
            }
            else
            {
                timeRemainingText.text = "Time: 00:00";
            }
        }
    }
    
    public void TriggerWin()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        
        // Play winner sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWinnerSound();
        }
        
        // Show win panel
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        // Update win text with Stanford-themed message
        if (winText != null)
        {
            float finalClicks = GlobalInjector.inventoryManager.Clicks;
            float timeElapsed = Time.time - gameStartTime;
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            
            winText.text = $"🎓 CONGRATULATIONS! 🎓\n\n" +
                          $"You've graduated from Stanford University!\n\n" +
                          $"Final Stats:\n" +
                          $"Total Clicks: {finalClicks:F0}\n" +
                          $"Time Played: {minutes}:{seconds:00}\n" +
                          $"CPS: {GlobalInjector.inventoryManager.GetTotalClicksPerSecond():F1}\n\n" +
                          $"You are now a true Stanford Cardinal! 🌲";
        }
        
        Debug.Log("Player won the game! Stanford graduation achieved!");
    }
    
    public void TriggerGameOver(string reason = "Game Over!")
    {
        if (gameEnded) return;
        
        gameEnded = true;
        
        // Play game over sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOverSound();
        }
        
        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        // Update game over text
        if (gameOverText != null)
        {
            float finalClicks = GlobalInjector.inventoryManager != null ? GlobalInjector.inventoryManager.Clicks : 0;
            gameOverText.text = $"{reason}\n\n" +
                               $"You reached {finalClicks:F0} clicks\n" +
                               $"Better luck next semester!";
        }
        
        Debug.Log($"Game Over: {reason}");
    }
    
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GoToMainMenu()
    {
        // Load the title scene
        SceneManager.LoadScene("01_Title");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // Public methods for external triggers
    public void SetWinCondition(float clicks)
    {
        winConditionClicks = clicks;
    }
    
    public void EnableTimeLimit(float timeInSeconds)
    {
        useTimeLimit = true;
        gameTimeLimit = timeInSeconds;
    }
    
    public void DisableTimeLimit()
    {
        useTimeLimit = false;
    }
}