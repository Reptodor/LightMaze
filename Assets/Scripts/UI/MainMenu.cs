using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private StateSwitch _stateSwitch;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private float _buttonsSize;
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _interval;

    public void Show()
    {
        Sequence animation = DOTween.Sequence();

        animation.Append(_buttons[0].transform.DOScale(_buttonsSize, _appearanceDuration).From(0).SetEase(Ease.OutBounce)).
                  AppendInterval(_interval).
                  Append(_buttons[1].transform.DOScale(_buttonsSize, _appearanceDuration).From(0).SetEase(Ease.OutBounce)).
                  AppendInterval(_interval).
                  Append(_buttons[2].transform.DOScale(_buttonsSize, _appearanceDuration).From(0).SetEase(Ease.OutBounce));

    }

    public void Hide()
    {
        Sequence animation = DOTween.Sequence();

        animation.Append(_buttons[0].transform.DOScale(0, _appearanceDuration).From(_buttonsSize).SetEase(Ease.InBack)).
                  AppendInterval(_interval).
                  Append(_buttons[1].transform.DOScale(0, _appearanceDuration).From(_buttonsSize).SetEase(Ease.InBack)).
                  AppendInterval(_interval).
                  Append(_buttons[2].transform.DOScale(0, _appearanceDuration).From(_buttonsSize).SetEase(Ease.InBack))
                  .AppendCallback(() => gameObject.SetActive(false));
    }

    public void Play()
    {
        Debug.Log("LoadLevel");
        Debug.Log("LoadCompleted");
        _stateSwitch.EnterIn<LevelInitializationState>();
    }

    public void OpenSettings()
    {
        _stateSwitch.EnterIn<SettingsMenuState>();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
