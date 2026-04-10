using UnityEngine;

public class MovingPhysicsObject : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private bool m_hasRigidbody;

    private Vector3 m_origin;
    private float m_speedX;
    private float m_speedY;
    private float m_rangeX;
    private float m_rangeY;
    private float m_phaseX;
    private float m_phaseY;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_hasRigidbody = m_rigidbody != null;

        m_origin = transform.position;
        m_speedX = Random.Range(0.5f, 2.5f);
        m_speedY = Random.Range(0.4f, 2.0f);
        m_rangeX = Random.Range(0.5f, 2.5f);
        m_rangeY = Random.Range(0.5f, 2.0f);
        m_phaseX = Random.Range(0f, Mathf.PI * 2f);
        m_phaseY = Random.Range(0f, Mathf.PI * 2f);
    }

    private void FixedUpdate()
    {
        float t = Time.time;
        Vector2 targetPosition = new Vector2(
            m_origin.x + Mathf.Sin(t * m_speedX + m_phaseX) * m_rangeX,
            m_origin.y + Mathf.Sin(t * m_speedY + m_phaseY) * m_rangeY);

        if (m_hasRigidbody)
        {
            m_rigidbody.MovePosition(targetPosition);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
