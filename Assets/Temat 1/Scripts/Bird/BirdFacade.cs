using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdFacade : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb = null;
    [SerializeField] private Animator m_animatorBody = null;

    private Transform m_visualTransform;
    private Vector3 m_baseScale;
    private bool m_isAlive = true;

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
    }

    private void Die()
    {
        m_isAlive = false;
        m_rb.linearVelocity = Vector2.zero;
        m_rb.gravityScale = 0f;
        GameManager.Get.GameOver();
    }
}
