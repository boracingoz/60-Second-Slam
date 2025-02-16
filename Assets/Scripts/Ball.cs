using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _ballAudio;
    private Rigidbody _ballRb; 
    private void Start()
    {
        _ballRb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket") || other.CompareTag("SecondBasket")) 
        {
            if (_ballRb.velocity.y > 0) 
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayBasketSound();
                }
                return;
            }

            _gameManager.Basket(transform.position);
        }
        else if (other.CompareTag("GameOver"))
        {
            _gameManager.GameOver();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBallCollisionSound();
        }
    }
}
