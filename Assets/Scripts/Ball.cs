using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _ballAudio; 

   void OnTriggerEnter(Collider other)
   {

        if (other.CompareTag("Basket"))
        {
            _gameManager.Basket(transform.position);
        }
        else if(other.CompareTag("GameOver"))
        {
           _gameManager.GameOver();
        }
   }


   void OnCollisionEnter(Collision other)
   {
        _ballAudio.Play();
   }
}
