using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 m_originalPosition;

    private void OnEnable()
    {
        m_originalPosition = transform.localPosition;
        GameManager.Get.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.Get.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < GameManagerData.Get.CameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * GameManagerData.Get.CameraShakeMagnitude;
            float y = Random.Range(-1f, 1f) * GameManagerData.Get.CameraShakeMagnitude;

            transform.localPosition = m_originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = m_originalPosition;
    }
}
