using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class GameManager : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _hoop;
    [SerializeField] private GameObject _widdenHoop;
    [SerializeField] private GameObject _dualHoopPowerUp; 
    [SerializeField] private GameObject _secondHoop; 
    [SerializeField] private GameObject[] _powerUpLocation;
    [SerializeField] private GameObject[] _dualHoopLocations;

    [Header("Partical Effects")]
    [SerializeField] private ParticleSystem[] _effects;

    [Header("UI")]
    [SerializeField] private Image[] _targetImage;
    [SerializeField] private Sprite _missionCheckSprite;
    [SerializeField] private int _targetBall;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private TextMeshProUGUI _levelNO;
    [SerializeField] private Button _backToMenuButton;

    private int _ballScore;

    void Start()
    {
        _levelNO.text = "LEVEL: " + SceneManager.GetActiveScene().name;

        for (int i = 0; i < _targetBall; i++)
        {
            _targetImage[i].gameObject.SetActive(true);
        }

        Invoke("PowerUpsLocation", 3f);
        Invoke("SpawnDualHoopPowerUp", 5f); 

        if (_backToMenuButton != null)
        {
            _backToMenuButton.onClick.AddListener(BackToMainMenu);
        }
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
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x - .5f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_platform.transform.position.x < 7.3)
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x + .5f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateDualHoop(Vector3.zero);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && _secondHoop != null && _secondHoop.activeSelf)
        {
            Basket(_secondHoop.transform.position);
        }
#endif
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
        int secondRandomNumber;
        
        do {
            secondRandomNumber = Random.Range(0, _powerUpLocation.Length - 1);
        } while (secondRandomNumber == randomNumber);

        _widdenHoop.transform.position = _powerUpLocation[randomNumber].transform.position;
        _widdenHoop.SetActive(true);
    }

    void SpawnDualHoopPowerUp()
    {
        if (_dualHoopPowerUp != null && _dualHoopLocations != null && _dualHoopLocations.Length > 0)
        {
            int randomLocation = Random.Range(0, _dualHoopLocations.Length);
            _dualHoopPowerUp.transform.position = _dualHoopLocations[randomLocation].transform.position;
            _dualHoopPowerUp.SetActive(true);
        }
    }

    public void CreateDualHoop(Vector3 powerUpPos)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWidenHoopSound();
        }

        if (_effects != null && _effects.Length > 1)
        {
            _effects[1].transform.position = powerUpPos;
            _effects[1].gameObject.SetActive(true);
        }
        
        StartCoroutine(DualHoopCoroutine());
    }

    private IEnumerator DualHoopCoroutine()
    {
        if (_secondHoop != null)
        {
            _secondHoop.SetActive(true);

            if (_effects != null && _effects.Length > 1)
            {
                _effects[1].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(10f);

            _secondHoop.SetActive(false);
        }
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
        Time.timeScale = 0;
        AudioManager.Instance.PlayWinSound();
        _panels[1].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        AudioManager.Instance.PlayGameOverSound();
        _panels[2].SetActive(true);
    }
    
    public void WidenTheHoop(Vector3 pos)
    {
        AudioManager.Instance.PlayWidenHoopSound();
        _effects[1].transform.position = pos;
        _effects[1].gameObject.SetActive(true);
        StartCoroutine(WidenHoopCoroutine());
    }

    private IEnumerator LoadSceneWithAd(int sceneIndex)
    {
        Time.timeScale = 1;
        
        if (YandexGame.SDKEnabled)
        {
            YandexGame.FullscreenShow();
            yield return new WaitForSeconds(0.5f);
        }
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllSounds();
        }
        
        SceneManager.LoadScene(sceneIndex);
    }

    public void Buttons(string value)
    {
        switch (value)
        {
            case "Stop":
                Time.timeScale = 0;
                _panels[0].SetActive(true);
                break;

            case "Resume":
                Time.timeScale = 1;
                _panels[0].SetActive(false);
                break;

            case "Try":
                StartCoroutine(LoadSceneWithAd(SceneManager.GetActiveScene().buildIndex));
                break;

            case "Next":
                StartCoroutine(LoadSceneWithAd(SceneManager.GetActiveScene().buildIndex + 1));
                break;

            case "Settings":
                Time.timeScale = 1;
                _panels[0].SetActive(false);
                break;

            case "Exit":
                Time.timeScale = 1;
                Application.Quit();
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

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        _ballScore = 0;
        
        foreach (GameObject panel in _panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }
        
        foreach (Image targetImg in _targetImage)
        {
            if (targetImg != null)
                targetImg.sprite = null;
        }

        SceneManager.LoadScene("Menu");
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllSounds();
        }
    }
}