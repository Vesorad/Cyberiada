using UnityEngine;

public class PooledProjectile : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 2f;

    private float m_spawnTime;
    private ProjectilePoolDemo m_owner;

    public void Init(ProjectilePoolDemo owner)
    {
        m_owner = owner;
        m_spawnTime = Time.time;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Time.time - m_spawnTime >= m_lifetime)
        {
            if (m_owner != null)
            {
                m_owner.ReturnProjectile(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
