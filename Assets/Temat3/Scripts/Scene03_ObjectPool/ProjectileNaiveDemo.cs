using Unity.Profiling;
using UnityEngine;

public class ProjectileNaiveDemo : MonoBehaviour
{
    [Header("Konfiguracja")]
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_lifetime = 2f;
    [SerializeField] private float m_spawnInterval = 0.05f;

    private float m_nextSpawnTime;
    private Vector2 m_spawnArea;
    private ProfilerRecorder m_gcRecorder;

    private void Start()
    {
        var cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        m_spawnArea = new Vector2(height * cam.aspect, height);
    }

    private void OnEnable()
    {
        m_gcRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "GC.Collect");
    }

    private void OnDisable()
    {
        m_gcRecorder.Dispose();
    }

    private void Update()
    {
        if (m_gcRecorder.Valid)
        {
            double gcTimeMs = m_gcRecorder.LastValue * 1e-6;
            Debug.Log($"GC Time: {gcTimeMs:F3} ms");
        }

        if (Time.time >= m_nextSpawnTime)
        {
            SpawnProjectile();
            m_nextSpawnTime = Time.time + m_spawnInterval;
        }
    }

    private void SpawnProjectile()
    {
        Vector3 pos = new Vector3(
            Random.Range(-m_spawnArea.x / 2f, m_spawnArea.x / 2f),
            Random.Range(-m_spawnArea.y / 2f, m_spawnArea.y / 2f),
            0f);
        var go = Instantiate(m_projectilePrefab, pos, Quaternion.identity);
        Destroy(go, m_lifetime);
    }
}
