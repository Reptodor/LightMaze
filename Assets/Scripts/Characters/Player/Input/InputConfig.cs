using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInputConfig", menuName = "Configs/Input")]
public class InputConfig : ScriptableObject
{
    [SerializeField] private KeyCode _speedBoostKey;
    [SerializeField] private KeyCode _flameBoostKey;

    public KeyCode SpeedBoostKey => _speedBoostKey;
    public KeyCode FlameBoostKey => _flameBoostKey;

    private void OnValidate()
    {
        if (_speedBoostKey == KeyCode.None)
            throw new ArgumentNullException(nameof(_speedBoostKey), "Speed boost key cannot be none");
        
        if (_flameBoostKey == KeyCode.None)
            throw new ArgumentNullException(nameof(_flameBoostKey), "Flame boost key cannot be none");
    }
}
