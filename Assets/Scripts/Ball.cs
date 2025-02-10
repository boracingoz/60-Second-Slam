using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _ballAudio;
    private Rigidbody _ballRb; // Rigidbody referansı

    private void Start()
    {
        _ballRb = GetComponent<Rigidbody>(); // Rigidbody bileşenini al
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket")) 
        {
            if (_ballRb.velocity.y < 0) // Eğer top aşağı doğru hareket ediyorsa sayı sayma
            {
                return;
            }

            _gameManager.Basket(transform.position);
        }
        else if (other.CompareTag("GameOver")) // Buradaki hatalı else yapısını düzelttim
        {
            _gameManager.GameOver();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        _ballAudio.Play();
    }
}
