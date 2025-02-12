using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MaınMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    [Header("Language")]
    public TMP_Dropdown languageDropdown;
    private bool isMuted = false;
    private float lastVolume = 1f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float volume = PlayerPrefs.GetFloat("GameVolume");
            SetVolume(volume);
            volumeSlider.value = volume;
        }

        if (PlayerPrefs.HasKey("IsMuted"))
        {
            isMuted = PlayerPrefs.GetInt("IsMuted") == 1;
            muteToggle.isOn = isMuted;
            SetMute(isMuted);
        }

        if (PlayerPrefs.HasKey("Language"))
        {
            languageDropdown.value = PlayerPrefs.GetInt("Language");
        }

        ShowMainMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayerPrefs.Save();
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("GameVolume", volume);
        lastVolume = volume;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        SetMute(isMuted);
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
    }

    private void SetMute(bool mute)
    {
        if (mute)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
        }
        else
        {
            SetVolume(lastVolume);
        }
    }

    public void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("Language", languageIndex);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
