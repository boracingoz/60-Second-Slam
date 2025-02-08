using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Object")]
   [SerializeField] private GameObject _platform;
   [SerializeField] private GameObject _hoop;
   [SerializeField] private GameObject _widdenHoop;
   [SerializeField] private GameObject[] _powerUpLocation;

   [Header("Audio")]
   [SerializeField] private AudioSource[] _audios;

   [Header("Partical Effects")]
   [SerializeField] private ParticleSystem[] _effects;
   

   [Header("UI")]
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

        Invoke("PowerUpsLocation", 3f);
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

        void PowerUpsLocation()
        {
            int randomNumber = Random.Range(0,_powerUpLocation.Length-1);

            _widdenHoop.transform.position = _powerUpLocation[randomNumber].transform.position;
            _widdenHoop.SetActive(true);
        }

        public void Basket(Vector3 pos)
        {
            _ballScore++;
            _targetImage[_ballScore - 1 ].sprite = _missionCheckSprite;
            _audios[4].Play();
            _effects[0].transform.position = pos;
            _effects[0].gameObject.SetActive(true);
            if (_targetBall == _ballScore)
            {
                Win();
            }
        }

        void Win()
        {
            _audios[3].Play();
            Debug.Log("Win!");
        }

        public void GameOver()
        {
            _audios[2].Play();
            Debug.Log("Game Over!");
        }
        public void WidenTheHoop(Vector3 pos)
        {
            _audios[1].Play();
            _effects[1].transform.position = pos;
            _effects[1].gameObject.SetActive(true);
            StartCoroutine(WidenHoopCoroutine());
        }

        private IEnumerator WidenHoopCoroutine()
        {
            Vector3 originalScale = _hoop.transform.localScale;
            _hoop.transform.localScale = new Vector3(95f, 95f, 95f);
            
            yield return new WaitForSeconds(10f);
            
            _hoop.transform.localScale = originalScale;
        }
}
