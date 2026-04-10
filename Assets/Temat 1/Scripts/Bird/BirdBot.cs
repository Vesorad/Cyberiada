using UnityEngine;

public class BirdBot : MonoBehaviour
{
    [SerializeField] private BirdFacade m_bird;
    [SerializeField] private float m_flapThreshold = 0.5f;
    [SerializeField] private float m_flapCooldown = 0.15f;
    [SerializeField] private float m_defaultTargetY = 0f;

    private float m_cooldownTimer;

    private void Reset()
    {
        m_bird = GetComponent<BirdFacade>();
    }

    private void Update()
    {
        if (GameManagerData.Get.BotEnabled == false || GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }

        HandleAI();
    }

    private void HandleAI()
    {
        m_cooldownTimer -= Time.deltaTime;

        if (m_cooldownTimer > 0f)
        {
            return;
        }

        Pipe nearest = GameManager.Get.GetNearestPipeAhead(transform.position.x);
        float targetY = nearest != null ? nearest.GapCenterY : m_defaultTargetY;

        if (transform.position.y < targetY - m_flapThreshold)
        {
            m_bird.TriggerFlap();
            m_cooldownTimer = m_flapCooldown;
        }
    }
}
