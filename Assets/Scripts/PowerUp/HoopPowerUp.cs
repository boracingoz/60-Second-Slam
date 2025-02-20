using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoopPowerUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private int _startTime;
    [SerializeField] GameManager _gameManager;

    IEnumerator Start()
    {
        _time.text = _startTime.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _startTime--;
            _time.text = _startTime.ToString();
            if (_startTime == 0)
            {
                gameObject.SetActive(false);
            }
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _gameManager.WidenTheHoop(transform.position);
    }
}
