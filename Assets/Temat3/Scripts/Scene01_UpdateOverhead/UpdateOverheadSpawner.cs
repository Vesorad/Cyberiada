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
            float z = Random.Range(0 / 2f, 15);

            var go = Instantiate(m_prefab, new Vector3(x, y, z), Quaternion.identity);
            go.name = $"{typeof(T).Name}_{i}";
            go.AddComponent<T>();
        }
    }

    private void SpawnSingle()
    {
        var go = Instantiate(m_prefab, Vector3.zero, Quaternion.identity);
        go.name = "SingleUpdater";
        go.AddComponent<SingleUpdaterObject>();
    }
}
