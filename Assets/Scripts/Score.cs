using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static EventHandler OnScoreChange;
    public static EventHandler OnScoreTreshold;

    [SerializeField] private int _scoreToAdd = 10;
    private readonly int _scoreTreshold = 100;

    [SerializeField] private float _scoreTimerMax = 1f;
    private float _scoreTimer;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsGamePaused())
        {
            return;
        }

        _scoreTimer += Time.deltaTime;

        if (_scoreTimer >= _scoreTimerMax)
        {
            _scoreTimer = 0;
            GameData.Instance.Score += _scoreToAdd;

            OnScoreChange?.Invoke(this, EventArgs.Empty);

            if (GameData.Instance.BestScore < GameData.Instance.Score)
            {
                GameData.Instance.BestScore = GameData.Instance.Score;
            }

            //triggering an event every specified time (e.g. speeding up the player)
            if (GameData.Instance.Score % _scoreTreshold == 0)
            {
                OnScoreTreshold?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
