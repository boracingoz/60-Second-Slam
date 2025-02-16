using System.Collections;
using UnityEngine;
using TMPro;

public class DualHoopPowerUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText; 
    [SerializeField] private int _startTime = 10; 
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        if (_timeText == null)
        {
            _timeText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        _startTime = 10;
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
        while (_startTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _startTime--;
            UpdateTimeText();
            
            if (_startTime <= 0)
            {
                gameObject.SetActive(false);
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

            if (_gameManager != null)
            {
                _gameManager.CreateDualHoop(transform.position);
            }
        }
    }
}
