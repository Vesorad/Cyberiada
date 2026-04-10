using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private Button m_restartButton;

    private void Start()
    {
        m_restartButton.onClick.AddListener(OnRestartClicked);
        GameManager.Get.OnGameOver += OnGameOver;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_restartButton.onClick.RemoveListener(OnRestartClicked);
        GameManager.Get.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        m_scoreText.text = GameManager.Get.Score.ToString();
        gameObject.SetActive(true);
    }

    private void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
