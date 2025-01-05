using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _barFilling;

    private void OnValidate()
    {
        if(_barFilling == null)
            _barFilling = GetComponent<Image>();
    }

    public void Display(float valuePercentage)
    {
        _barFilling.fillAmount = valuePercentage;
    }
}
