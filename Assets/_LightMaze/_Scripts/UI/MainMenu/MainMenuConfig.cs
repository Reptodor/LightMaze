using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMainMenuConfig", menuName = "Configs/UI/MainMenu")]
public class MainMenuConfig : ScriptableObject
{
    [SerializeField] private float _buttonsSize;
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _interval;

    public float ButtonsSize => _buttonsSize;
    public float AppearanceDuration => _appearanceDuration;
    public float Interval => _interval;




    private void OnValidate()
    {
        if(_buttonsSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_buttonsSize), "Buttons size must be greater than zero");
            
        if(_appearanceDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_appearanceDuration), "Appearance duration must be greater than zero");
    }
}
