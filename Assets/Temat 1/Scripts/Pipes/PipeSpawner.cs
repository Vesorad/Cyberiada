using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner
{
    private ObjectPool<Pipe> m_pool;
    private readonly List<Pipe> m_activePipes = new List<Pipe>();
    private float m_timer;

    public void Initialize(Transform parent)
    {
        m_pool = new ObjectPool<Pipe>(AssetsManager.Get.PipePrefab, 10, parent);
    }

    public void Tick(float deltaTime)
    {
        if (GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }

        m_timer += deltaTime;

        if (m_timer >= GameManagerData.Get.PipeSpawnInterval / Mathf.Sqrt(GameManager.Get.GameSpeed))
        {
            m_timer = 0f;
            Spawn();
        }
    }

    public void Reset()
    {
        foreach (Pipe pipe in m_activePipes)
        {
            m_pool.Return(pipe);
        }

        m_activePipes.Clear();
        m_timer = GameManagerData.Get.PipeSpawnInterval - GameManagerData.Get.FirstPipeDelay;
    }

    private void Spawn()
    {
        float spawnX = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float y = Random.Range(-GameManagerData.Get.PipeSpawnYRange, GameManagerData.Get.PipeSpawnYRange);
        Pipe pipe = m_pool.Get(new Vector3(spawnX, y, 0f));
        m_activePipes.Add(pipe);
    }

    public void Return(Pipe pipe)
    {
        m_activePipes.Remove(pipe);
        m_pool.Return(pipe);
    }

    public Pipe GetNearestAhead(float birdX)
    {
        Pipe nearest = null;
        float minDist = float.MaxValue;

        foreach (Pipe pipe in m_activePipes)
        {
            float dist = pipe.GapCenterX - birdX;

            if (dist > 0f && dist < minDist)
            {
                minDist = dist;
                nearest = pipe;
            }
        }
        
        return nearest;
    }
}
