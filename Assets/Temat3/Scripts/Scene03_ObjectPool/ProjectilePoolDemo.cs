using UnityEngine;

public class ProjectilePoolDemo : MonoBehaviour
{
    [Header("Konfiguracja")]
    [SerializeField] private PooledProjectile m_projectilePrefab;
    [SerializeField] private int m_initialPoolSize = 30;
    [SerializeField] private float m_spawnInterval = 0.05f;

    private UnityEngine.Pool.ObjectPool<PooledProjectile> m_pool;
    private float m_nextSpawnTime;
    private Vector2 m_spawnArea;

    private void Start()
    {
        var cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        m_spawnArea = new Vector2(height * cam.aspect, height);

        m_pool = new UnityEngine.Pool.ObjectPool<PooledProjectile>(
            createFunc: () =>
            {
                var obj = Object.Instantiate(m_projectilePrefab, transform);
                obj.gameObject.SetActive(false);
                return obj;
            },
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: obj => Destroy(obj.gameObject),
            defaultCapacity: m_initialPoolSize
        );

        for (int i = 0; i < m_initialPoolSize; i++)
        {
            m_pool.Release(m_pool.Get());
        }
    }

    private void Update()
    {
        if (Time.time >= m_nextSpawnTime)
        {
            SpawnProjectile();
            m_nextSpawnTime = Time.time + m_spawnInterval;
        }
    }

    private void SpawnProjectile()
    {
        var projectile = m_pool.Get();
        projectile.transform.position = new Vector2(
            Random.Range(-m_spawnArea.x / 2f, m_spawnArea.x / 2f),
            Random.Range(-m_spawnArea.y / 2f, m_spawnArea.y / 2f));
        projectile.Init(this);
    }

    public void ReturnProjectile(PooledProjectile projectile)
    {
        m_pool.Release(projectile);
    }
}
