using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _ballAudio;
    private Rigidbody _ballRb;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _canScore = true;
    private bool _isPassingThroughBasket = false;
    
    private void Start()
    {
        _ballRb = GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Basket") || other.CompareTag("SecondBasket")) && _canScore)
        {
            _isPassingThroughBasket = true;
        }
        else if (other.CompareTag("GameOver"))
        {
            _gameManager.GameOver();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Basket") || other.CompareTag("SecondBasket")) && _isPassingThroughBasket && _canScore)
        {
            if (_ballRb.velocity.y < 0)
            {
                _canScore = false;
                _isPassingThroughBasket = false;
                
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayBasketSound();
                }
                
                _gameManager.Basket(transform.position);
                StartCoroutine(RespawnBall());
            }
        }
    }

    private IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(0.35f);
        
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        
        _ballRb.velocity = Vector3.zero;
        _ballRb.angularVelocity = Vector3.zero;
        
        _canScore = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBallCollisionSound();
        }
    }
}
