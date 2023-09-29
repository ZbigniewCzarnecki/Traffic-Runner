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

    bool _unpausing;

    [Header("Feedback")]
    [SerializeField] private AudioClip _gameStartSound;
    [SerializeField] private AudioClip _gameEndSound;

    public enum GameState
    {
        WaitForInput,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private GameState _gameState = GameState.WaitForInput;

    private float _countdownToStartTimer = 3f;
    private readonly float _countdownToUnpauseTimerMax = 3f;
    private float _countdownToUnpauseTimer;

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

    private void Start()
    {
        _countdownToUnpauseTimer = _countdownToUnpauseTimerMax;
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

                    AudioManager.Instance.PlaySound(_gameStartSound);

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                if (_unpausing)
                {
                    _countdownToUnpauseTimer -= Time.unscaledDeltaTime;

                    if(_countdownToUnpauseTimer <= 0)
                    {
                        _unpausing = false;

                        _isGamePaused = false;

                        Time.timeScale = 1;

                        _countdownToUnpauseTimer = _countdownToUnpauseTimerMax;

                        CountdownToUnpauseUI.Instance.Hide();

                        AudioManager.Instance.PlaySound(_gameStartSound);

                        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
                    }
                }
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
            _unpausing = true;

            CountdownToUnpauseUI.Instance.Show();
        }
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        AudioManager.Instance.PlaySound(_gameEndSound);

        GameData.Instance.UpdateDataBeforeSaving();
        GameData.Instance.SaveGameData();

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

    public float GetCountdownToUnpauseTimer()
    {
        return _countdownToUnpauseTimer;
    }
}
