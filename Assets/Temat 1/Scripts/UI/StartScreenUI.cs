using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private Button m_startButton;
    [SerializeField] private Button m_exitButton;

    private void OnEnable()
    {
        m_startButton.onClick.AddListener(OnStartClicked);
        m_exitButton.onClick.AddListener(OnExitClicked);
        GameManager.Get.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        m_startButton.onClick.RemoveListener(OnStartClicked);
        m_exitButton.onClick.RemoveListener(OnExitClicked);
        GameManager.Get.OnGameStarted -= OnGameStarted;
    }

    private void OnStartClicked()
    {
        GameManager.Get.StartGame();
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(false);
    }
}
