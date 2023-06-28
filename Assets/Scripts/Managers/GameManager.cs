using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGameStart;
    [SerializeField] private UnityEvent OnGameOver;

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public enum GameState
    {
        WaitForInput,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private GameState _gameState = GameState.WaitForInput;

    private float _countdownToStartTimer = 3f;

    private bool _isGamePaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one GameManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.WaitForInput:
                if (InputManager.Instance.IsPressWasPerformedThisFrame() && !InputManager.Instance.IsPointerOverUI())
                {
                    _gameState = GameState.CountdownToStart;

                    OnGameStart?.Invoke();

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;

                if (_countdownToStartTimer < 0)
                {
                    _gameState = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:

                break;
            case GameState.GameOver:
                
                break;
            default:
                break;
        }
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            Time.timeScale = 0;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        //DataManager.Instance.SaveData();

        OnGameOver?.Invoke();

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsWaitForInput()
    {
        return _gameState == GameState.WaitForInput;
    }

    public bool IsCountdownToStart()
    {
        return _gameState == GameState.CountdownToStart;
    }

    public bool IsGamePlaying()
    {
        return _gameState == GameState.GamePlaying;
    }

    public bool IsGameOver()
    {
        return _gameState == GameState.GameOver;
    }

    public bool IsGamePaused()
    {
        return _isGamePaused;
    }

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }
}
