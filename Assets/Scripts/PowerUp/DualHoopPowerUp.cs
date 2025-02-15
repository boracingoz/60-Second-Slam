using System.Collections;
using UnityEngine;
using TMPro;

public class DualHoopPowerUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText; 
    [SerializeField] private int _startTime = 10; 
    [SerializeField] private GameManager _gameManager;
    private bool isCountingStarted = false;

    private void Awake()
    {
        // Başlangıçta text komponentini bul
        if (_timeText == null)
        {
            _timeText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        _startTime = 10;
        isCountingStarted = false;
        UpdateTimeText();
        StartCoroutine(CountdownRoutine());
    }

    private void UpdateTimeText()
    {
        if (_timeText != null)
        {
            _timeText.text = _startTime.ToString();
        }
    }

    private IEnumerator CountdownRoutine()
    {
        isCountingStarted = true;
        while (_startTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _startTime--;
            UpdateTimeText();
            
            if (_startTime <= 0)
            {
                gameObject.SetActive(false);
                isCountingStarted = false;
                yield break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) 
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            isCountingStarted = false;

            if (_gameManager != null)
            {
                _gameManager.CreateDualHoop(transform.position);
            }
        }
    }
}
