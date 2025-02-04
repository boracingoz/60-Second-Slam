using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject _platform;
   
   [SerializeField] private Image[] _targetImage;
   [SerializeField] private Sprite _missionCheckSprite;
   [SerializeField] private int _targetBall;


   private int _ballScore;


   void Start()
   {
        for (int i = 0; i < _targetBall; i++)
        {
        
            _targetImage[i].gameObject.SetActive(true);

        }
   }
        
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (_platform.transform.position.x > -7.3)
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x - .3f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (_platform.transform.position.x < 7.3)
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x + .3f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
            }
        }

        public void Basket()
        {
            _ballScore++;
            _targetImage[_ballScore - 1 ].sprite = _missionCheckSprite;

            if (_targetBall == _ballScore)
            {
                //*win panel
                Debug.Log("You win!");
            }
        }

        public void GameOver()
        {
            //!gameover panel
            Debug.Log("Game Over!");
        }
}
