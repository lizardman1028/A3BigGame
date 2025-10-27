using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StanfordGoal
{
    public string title;
    public string description;
    public float targetClicks;
    public bool isCompleted;
    public UnityEvent onGoalCompleted;
}

public class GoalManager : MonoBehaviour
{
    [Header("Stanford University Goals")]
    [SerializeField] private List<StanfordGoal> goals = new List<StanfordGoal>();
    
    [Header("UI References")]
    [SerializeField] private Transform goalDisplayParent;
    [SerializeField] private GameObject goalDisplayPrefab;
    
    public static GoalManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeStanfordGoals();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        CheckGoalProgress();
    }
    
    private void InitializeStanfordGoals()
    {
        // Initialize Stanford-themed goals
        goals.Clear();
        
        goals.Add(new StanfordGoal
        {
            title = "Welcome to Stanford!",
            description = "Get your first 10 clicks",
            targetClicks = 10f,
            isCompleted = false
        });
        
        goals.Add(new StanfordGoal
        {
            title = "Getting Started",
            description = "Reach 100 clicks",
            targetClicks = 100f,
            isCompleted = false
        });
        
        goals.Add(new StanfordGoal
        {
            title = "Campus Explorer",
            description = "Achieve 1,000 clicks",
            targetClicks = 1000f,
            isCompleted = false
        });
        
        goals.Add(new StanfordGoal
        {
            title = "Stanford Enthusiast",
            description = "Reach 10,000 clicks",
            targetClicks = 10000f,
            isCompleted = false
        });
        
        goals.Add(new StanfordGoal
        {
            title = "Cardinal Legend",
            description = "Achieve 100,000 clicks",
            targetClicks = 100000f,
            isCompleted = false
        });
        
        goals.Add(new StanfordGoal
        {
            title = "Stanford Master",
            description = "Reach 1,000,000 clicks - You've mastered Stanford!",
            targetClicks = 1000000f,
            isCompleted = false
        });
    }
    
    private void CheckGoalProgress()
    {
        if (GlobalInjector.inventoryManager == null) return;
        
        float currentClicks = GlobalInjector.inventoryManager.Clicks;
        
        foreach (var goal in goals)
        {
            if (!goal.isCompleted && currentClicks >= goal.targetClicks)
            {
                CompleteGoal(goal);
            }
        }
    }
    
    private void CompleteGoal(StanfordGoal goal)
    {
        goal.isCompleted = true;
        
        // Play goal achievement sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGoalAchievedSound();
        }
        
        // Invoke any custom events
        goal.onGoalCompleted?.Invoke();
        
        // Log achievement (you can replace this with UI notifications)
        Debug.Log($"Goal Completed: {goal.title} - {goal.description}");
        
        // You could add UI notification here
        ShowGoalCompletedNotification(goal);
    }
    
    private void ShowGoalCompletedNotification(StanfordGoal goal)
    {
        // This is where you'd implement UI notifications
        // For now, we'll just log it
        Debug.Log($"🎉 ACHIEVEMENT UNLOCKED: {goal.title}!");
    }
    
    public List<StanfordGoal> GetAllGoals()
    {
        return goals;
    }
    
    public List<StanfordGoal> GetCompletedGoals()
    {
        return goals.FindAll(goal => goal.isCompleted);
    }
    
    public List<StanfordGoal> GetIncompleteGoals()
    {
        return goals.FindAll(goal => !goal.isCompleted);
    }
}