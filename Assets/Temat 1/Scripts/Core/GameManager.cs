using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action<int> OnScoreChanged;

    public GameState CurrentState { get; private set; }
    public int Score { get; private set; }
    public int BestScore { get; private set; }
    public float GameSpeed { get; private set; } = 1f;

    private PipeSpawner m_pipeSpawner;

    protected override void Awake()
    {
        base.Awake();
        m_pipeSpawner = new PipeSpawner();
        m_pipeSpawner.Initialize(transform);
    }

    private void Update()
    {
        m_pipeSpawner.Tick(Time.deltaTime);
    }

    public void StartGame()
    {
        Score = 0;
        GameSpeed = 1f;
        m_pipeSpawner.Reset();
        CurrentState = GameState.Playing;
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    public void AddPoint()
    {
        Score++;

        if (Score > BestScore)
        {
            BestScore = Score;
        }

        GameSpeed = Mathf.Min(1f + Score * GameManagerData.Get.SpeedIncreasePerPoint, GameManagerData.Get.MaxGameSpeed);
        OnScoreChanged?.Invoke(Score);
    }

    public void ReturnPipe(Pipe pipe)
    {
        m_pipeSpawner.Return(pipe);
    }

    public Pipe GetNearestPipeAhead(float birdX)
    {
        return m_pipeSpawner.GetNearestAhead(birdX);
    }
}
