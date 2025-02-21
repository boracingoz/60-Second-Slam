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
            LoadSoundState();
            InitializeVolume();
            PlayGameLoopMusic();
        }
        else
        {
            if (volumeSlider != null)
            {
                volumeSlider.value = Instance.lastVolume;
                volumeSlider.onValueChanged.RemoveAllListeners();
                volumeSlider.onValueChanged.AddListener(Instance.SetVolume);
            }
            if (soundButtonImage != null)
            {
                soundButtonImage.sprite = Instance.isMuted ? Instance.soundOffSprite : Instance.soundOnSprite;
                soundButtonImage.color = Instance.isMuted ? Instance.mutedButtonColor : Instance.normalButtonColor;
            }
            if (!Instance.audioSources[0].isPlaying)
            {
                Instance.PlayGameLoopMusic();
            }
            Destroy(gameObject);
        }
    }

    private void InitializeVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREFS, 1f);
        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        ApplyVolumeToSources(savedVolume);
    }

    private void LoadSoundState()
    {
        isMuted = PlayerPrefs.GetInt(SOUND_STATE_PREFS, 0) == 1;
        lastVolume = PlayerPrefs.GetFloat(VOLUME_PREFS, 1f);
        
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
            soundButtonImage.color = isMuted ? mutedButtonColor : normalButtonColor;
        }
    }

    private void ApplyVolumeToSources(float volume)
    {
        float effectiveVolume = isMuted || Mathf.Approximately(volume, 0f) ? 0f : volume;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.volume = effectiveVolume;
            }
        }
    }

    public void SetVolume(float volume)
    {
        if (Mathf.Approximately(volume, 0f))
        {
            if (!isMuted)
            {
                isMuted = true;
                if (soundButtonImage != null)
                {
                    soundButtonImage.sprite = soundOffSprite;
                    soundButtonImage.color = mutedButtonColor;
                }
            }
        }
        else if (isMuted)
        {
            isMuted = false;
            if (soundButtonImage != null)
            {
                soundButtonImage.sprite = soundOnSprite;
                soundButtonImage.color = normalButtonColor;
            }
        }

        lastVolume = volume;
        ApplyVolumeToSources(volume);

        PlayerPrefs.SetFloat(VOLUME_PREFS, volume);
        PlayerPrefs.SetInt(SOUND_STATE_PREFS, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
            soundButtonImage.color = isMuted ? mutedButtonColor : normalButtonColor;
        }

        ApplyVolumeToSources(lastVolume);
        
        PlayerPrefs.SetInt(SOUND_STATE_PREFS, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void StopAllSounds()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
                source.Stop();
        }
        PlayGameLoopMusic();
    }

    public void PlayGameLoopMusic()
    {
        if (audioSources.Length > 0 && !audioSources[0].isPlaying && !isMuted)
        {
            audioSources[0].loop = true;
            audioSources[0].volume = PlayerPrefs.GetFloat(VOLUME_PREFS, 1f);
            audioSources[0].Play();
        }
    }

    public void PlayWidenHoopSound()
    {
        if (audioSources.Length > 1)
            audioSources[1].Play();
    }

    public void PlayGameOverSound()
    {
        if (audioSources.Length > 2)
        {
            audioSources[0].Stop();
            audioSources[2].Play();
        }
    }

    public void PlayWinSound()
    {
        if (audioSources.Length > 3)
        {
            audioSources[0].Stop();
            audioSources[3].Play();
        }
    }

    public void PlayBallCollisionSound()
    {
        if (audioSources.Length > 4)
            audioSources[4].Play();
    }

    public void PlayBasketSound()
    {
        if (audioSources.Length > 5)
            audioSources[5].Play();
    }
}
