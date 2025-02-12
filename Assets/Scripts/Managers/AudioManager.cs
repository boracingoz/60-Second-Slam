using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] audioSources;

    [Header("UI Elements")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Color normalButtonColor = Color.white;
    [SerializeField] private Color mutedButtonColor = Color.red;

    private const string VOLUME_PREFS = "GameVolume";
    private const string SOUND_STATE_PREFS = "SoundState";
    private float lastVolume;
    private bool isMuted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeVolume();
            LoadSoundState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREFS, 1f);
        lastVolume = savedVolume;
        SetVolume(savedVolume);
        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void LoadSoundState()
    {
        isMuted = PlayerPrefs.GetInt(SOUND_STATE_PREFS, 0) == 1;
        UpdateSoundState();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        UpdateSoundState();
        PlayerPrefs.SetInt(SOUND_STATE_PREFS, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateSoundState()
    {
        if (isMuted)
        {
            lastVolume = volumeSlider != null ? volumeSlider.value : 1f;
            SetVolume(0f);
            if (soundButtonImage != null)
            {
                soundButtonImage.sprite = soundOffSprite;
                soundButtonImage.color = mutedButtonColor;
            }
        }
        else
        {
            SetVolume(lastVolume);
            if (soundButtonImage != null)
            {
                soundButtonImage.sprite = soundOnSprite;
                soundButtonImage.color = normalButtonColor;
            }
        }

        if (volumeSlider != null)
            volumeSlider.value = isMuted ? 0f : lastVolume;
    }

    public void SetVolume(float volume)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.volume = volume;
        }
        PlayerPrefs.SetFloat(VOLUME_PREFS, volume);
        PlayerPrefs.Save();
    }

    public void PlayBasketSound()
    {
        if (audioSources.Length > 4)
            audioSources[4].Play();
    }

    public void PlayWinSound()
    {
        if (audioSources.Length > 3)
            audioSources[3].Play();
    }

    public void PlayGameOverSound()
    {
        if (audioSources.Length > 2)
            audioSources[2].Play();
    }

    public void PlayWidenHoopSound()
    {
        if (audioSources.Length > 1)
            audioSources[1].Play();
    }
}
