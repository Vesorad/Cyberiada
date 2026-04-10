using UnityEngine;

public class UpdateOverheadSpawner : MonoBehaviour
{
    public enum SpawnMode
    {
        EmptyUpdate,
        DebugLogUpdate,
        SingleUpdater
    }

    [Header("Konfiguracja")]
    [SerializeField] private SpawnMode m_mode = SpawnMode.EmptyUpdate;
    [SerializeField] private int m_objectCount = 10000;
    [SerializeField] private GameObject m_prefab;

    private Vector2 m_spawnArea;

    private void Start()
    {
        m_spawnArea = GetScreenWorldSize();

        switch (m_mode)
        {
            case SpawnMode.EmptyUpdate:
                SpawnMany<EmptyUpdateObject>();
                break;
            case SpawnMode.DebugLogUpdate:
                SpawnMany<DebugLogUpdateObject>();
                break;
            case SpawnMode.SingleUpdater:
                SpawnSingle();
                break;
        }
    }

    private static Vector2 GetScreenWorldSize()
    {
        var cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;
        return new Vector2(width, height);
    }

    private void SpawnMany<T>() where T : MonoBehaviour
    {
        for (int i = 0; i < m_objectCount; i++)
        {
            float x = Random.Range(-m_spawnArea.x / 2f, m_spawnArea.x / 2f);
            float y = Random.Range(-m_spawnArea.y / 2f, m_spawnArea.y / 2f);
            var go = m_prefab != null
                ? Instantiate(m_prefab, new Vector3(x, y, 0f), Quaternion.identity)
                : new GameObject($"{typeof(T).Name}_{i}");
            go.name = $"{typeof(T).Name}_{i}";
            if (go.GetComponent<T>() == null)
            {
                go.AddComponent<T>();
            }
        }
    }

    private void SpawnSingle()
    {
        var go = m_prefab != null
            ? Instantiate(m_prefab, Vector3.zero, Quaternion.identity)
            : new GameObject("SingleUpdater");
        go.name = "SingleUpdater";
        if (go.GetComponent<SingleUpdaterObject>() == null)
        {
            go.AddComponent<SingleUpdaterObject>();
        }
    }
}
