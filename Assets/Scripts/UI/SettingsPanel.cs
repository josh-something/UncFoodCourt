using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    
    UIManager _uiManager;
    GameSettingsManager _gsManager;
    
    public Button _closeButton;
    public Slider _musicSlider;
    public Slider _sfxSlider;
    public TMP_Text _musicText;
    public TMP_Text _sfxText;
    void Start()
    {
        _uiManager = FindFirstObjectByType<UIManager>();
        if (_uiManager != null)
        {
            _closeButton.onClick.AddListener(_uiManager.CloseCurrentPanel);
        }
        
        _gsManager = GameSettingsManager.Instance;
        if (_gsManager != null)
        {
            _musicSlider.SetValueWithoutNotify(_gsManager.MusicVolume);
            _musicText.text = $"{_gsManager.MusicVolume}";
            _sfxSlider.SetValueWithoutNotify(_gsManager.SfxVolume);
            _sfxText.text = $"{_gsManager.SfxVolume}";
            
            _musicSlider.onValueChanged.AddListener(value => { _gsManager.MusicVolume = value; });
            _musicSlider.onValueChanged.AddListener(value => { _musicText.text = $"{value:N0}"; });
            _sfxSlider.onValueChanged.AddListener( value => { _gsManager.SfxVolume = value; } );
            _sfxSlider.onValueChanged.AddListener(value => { _sfxText.text = $"{value:N0}"; });
        }
    }

}
