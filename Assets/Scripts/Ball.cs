using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;

   void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Basket"))
        {
            _gameManager.Basket();
        }
        else if(other.CompareTag("GameOver"))
        {
           _gameManager.GameOver();
        }
   }
}
