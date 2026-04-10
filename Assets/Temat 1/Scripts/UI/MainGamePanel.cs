using System;
using TMPro;
using UnityEngine;

public class MainGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;

    private void Start()
    {
        GameManager.Get.OnGameStarted += OnGameStarted;
        GameManager.Get.OnScoreChanged += OnScoreChanged;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Get.OnGameStarted -= OnGameStarted;
        GameManager.Get.OnScoreChanged -= OnScoreChanged;
    }

    private void OnGameStarted()
    {
        m_scoreText.text = "0";
        gameObject.SetActive(true);
    }

    private void OnScoreChanged(int score)
    {
        m_scoreText.text = score.ToString();
    }
}
