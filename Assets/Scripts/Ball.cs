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
        if (other.CompareTag("Basket")) 
        {
            if (_ballRb.velocity.y > 0) 
            {
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
        _ballAudio.Play();
    }
}
