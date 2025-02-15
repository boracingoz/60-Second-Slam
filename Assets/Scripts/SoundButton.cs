using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Button button;
    private Image buttonImage;
    
    [SerializeField] private Color normalColor = Color.white;  // FFFFFF
    [SerializeField] private Color mutedColor = Color.red;    // FF0000

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        
        bool isMuted = PlayerPrefs.GetInt("SoundState", 0) == 1;
        buttonImage.color = isMuted ? mutedColor : normalColor;
        
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSound();
            bool isMuted = PlayerPrefs.GetInt("SoundState", 0) == 1;
            buttonImage.color = isMuted ? mutedColor : normalColor;
        }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}