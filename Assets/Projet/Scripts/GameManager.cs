using UnityEngine;
using UnityEngine.Events;

public enum GameMode { Defouloir, Recette }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    public float gameDuration = 300f; // 5 minutes
    public GameMode currentMode = GameMode.Defouloir;

    [Header("State")]
    public float currentTime;
    public int score;
    public bool isPlaying;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<float> OnTimeChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame(); // Call StartGame() so the Spawner starts immediately
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentTime -= Time.deltaTime;
            OnTimeChanged?.Invoke(currentTime);

            if (currentTime <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        ResetGame();
        isPlaying = true;
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        isPlaying = false;
        currentTime = 0;
        OnTimeChanged?.Invoke(currentTime);
        OnGameOver?.Invoke();
    }

    public void ResetGame()
    {
        isPlaying = false;
        currentTime = gameDuration;
        score = 0;
        OnScoreChanged?.Invoke(score);
        OnTimeChanged?.Invoke(currentTime);
        // If Recette mode, reset recipe here
    }

    public void AddScore(int points)
    {
        if (!isPlaying) return;
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public void AddTimePenalty(float penaltySeconds) // For bombs
    {
        if (!isPlaying) return;
        currentTime -= penaltySeconds;
        if (currentTime <= 0)
        {
            EndGame();
        }
    }
}
