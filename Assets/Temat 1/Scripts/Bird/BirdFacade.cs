using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdFacade : MonoBehaviour
{
    private const float FLAP_DURATION = 1.5f;


    [SerializeField] private Rigidbody2D m_rb = null;
    [SerializeField] private Animator m_animatorBody = null;

    private Transform m_visualTransform;
    private Vector3 m_baseScale;
    private bool m_isAlive = true;
    private float m_flapTimer = 0f;

    private void Reset()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        m_visualTransform = m_animatorBody.transform;
        if (m_animatorBody != null)
        {
            m_baseScale = m_animatorBody.transform.localScale;
        }
        else
        {
            Debug.LogError("Animator Body is not assigned in the inspector!", this);
        }
    }

    private void Start()
    {
        m_rb.gravityScale = 0f;
    }

    private void OnEnable()
    {
        GameManager.Get.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        GameManager.Get.OnGameStarted -= OnGameStarted;
    }


    private void OnGameStarted()
    {
        m_isAlive = true;
        m_rb.gravityScale = 1f;
        TriggerFlap();
    }

    private void Update()
    {
        if (m_isAlive == false || GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }
        m_flapTimer = Mathf.Max(m_flapTimer - Time.deltaTime, 0f);
        HandleInput();
        UpdateRotation();
    }

    private void FixedUpdate()
    {
        if (m_isAlive == false || GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }

        ClampFallSpeed();
    }

    private void UpdateRotation()
    {
        float progress = m_flapTimer / FLAP_DURATION;
        float angle = Mathf.Lerp(GameManagerData.Get.BirdMinAngle, GameManagerData.Get.BirdMaxAngle, progress);
        m_visualTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleInput()
    {
        if (GameManagerData.Get.BotEnabled == false &&
            (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame))
        {
            TriggerFlap();
        }
    }

    private void ClampFallSpeed()
    {
        m_rb.gravityScale = GameManager.Get.GameSpeed;
        float maxFall = GameManagerData.Get.MaxFallSpeed * GameManager.Get.GameSpeed;
        m_rb.linearVelocity = new Vector2(0f, Mathf.Max(m_rb.linearVelocity.y, maxFall));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tags.Score))
        {
            return;
        }

        Die();
    }

    public void TriggerFlap()
    {
        m_rb.linearVelocity = new Vector2(0f, GameManagerData.Get.FlapForce);
        m_flapTimer = FLAP_DURATION;
    }

    private void Die()
    {
        m_isAlive = false;
        m_rb.linearVelocity = Vector2.zero;
        m_rb.gravityScale = 0f;
        GameManager.Get.GameOver();
    }
}
