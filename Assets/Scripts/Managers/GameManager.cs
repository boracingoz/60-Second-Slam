using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _hoop;
    [SerializeField] private GameObject _widdenHoop;
    [SerializeField] private GameObject[] _powerUpLocation;

    [Header("Partical Effects")]
    [SerializeField] private ParticleSystem[] _effects;

    [Header("UI")]
    [SerializeField] private Image[] _targetImage;
    [SerializeField] private Sprite _missionCheckSprite;
    [SerializeField] private int _targetBall;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private TextMeshProUGUI _levelNO;


    private int _ballScore;


    void Start()
    {
        _levelNO.text = "LEVEL: " + SceneManager.GetActiveScene().name;

        for (int i = 0; i < _targetBall; i++)
        {

            _targetImage[i].gameObject.SetActive(true);

        }

        Invoke("PowerUpsLocation", 3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

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

    private void PauseGame()
    {
        if (!_panels[0].activeSelf)
        {
            Time.timeScale = 0;
            _panels[0].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _panels[0].SetActive(false);
        }
    }

    void PowerUpsLocation()
    {
        int randomNumber = Random.Range(0, _powerUpLocation.Length - 1);

        _widdenHoop.transform.position = _powerUpLocation[randomNumber].transform.position;
        _widdenHoop.SetActive(true);
    }

    public void Basket(Vector3 pos)
    {
        _ballScore++;
        _targetImage[_ballScore - 1].sprite = _missionCheckSprite;
        AudioManager.Instance.PlayBasketSound();
        _effects[0].transform.position = pos;
        _effects[0].gameObject.SetActive(true);
        if (_targetBall == _ballScore)
        {
            Win();
        }
    }

    void Win()
    {
        AudioManager.Instance.PlayWinSound();
        _panels[1].SetActive(true);
        PlayerPrefs.GetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale=0;
    }

    public void GameOver()
    {
        AudioManager.Instance.PlayGameOverSound();
        _panels[2].SetActive(true);
        Time.timeScale=0;
    }
    
    public void WidenTheHoop(Vector3 pos)
    {
        AudioManager.Instance.PlayWidenHoopSound();
        _effects[1].transform.position = pos;
        _effects[1].gameObject.SetActive(true);
        StartCoroutine(WidenHoopCoroutine());
    }

    public void Buttons(string value)
    {
        switch (value)
        {
            case "Stop":
                Time.timeScale=0;
                _panels[0].SetActive(true);
                break;

            case "Resume":
                Time.timeScale=1;
                _panels[0].SetActive(false);
                break;

            case "Try":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale=1;
                break;

            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale=1;
                break;

            case "Settings":
            //settings panel
                Time.timeScale=1;
                _panels[0].SetActive(false);
                break;

            case "Exit":
                Application.Quit();
                Debug.Log("Exit game...");
                //Are you sure panel
                break;
        }
    }

    private IEnumerator WidenHoopCoroutine()
    {
        Vector3 originalScale = _hoop.transform.localScale;
        _hoop.transform.localScale = new Vector3(95f, 95f, 95f);

        yield return new WaitForSeconds(10f);

        _hoop.transform.localScale = originalScale;
    }
}
