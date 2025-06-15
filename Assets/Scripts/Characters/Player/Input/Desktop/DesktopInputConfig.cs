using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDesktopInputConfig", menuName = "Configs/Input/Desktop")]
public class DesktopInputConfig : ScriptableObject
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
