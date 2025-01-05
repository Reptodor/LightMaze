using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private MainMenu _mainMenu;
    private bool _isSoundActive;
    private bool _isAudioActive;

    public void SwitchSound()
    {
        if(_isSoundActive)
            _isSoundActive = false;
        else
            _isSoundActive = true;
    }

    public void SwitchAuido()
    {
        if(_isAudioActive)
            _isAudioActive = false;
        else
            _isAudioActive = true;
    }

    public void Back()
    {
        gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }
}
