using System.Collections;
using UnityEngine;

public class ThirdLevelBootstrap : SecondLevelBootstrap
{
    [Header("Slime")]
    [SerializeField] private Slime[] _slimes;
    [SerializeField] private MovementConfig _slimeMovementConfig;

    public override IEnumerator Initialize()
    {
        yield return base.Initialize();

        foreach(Slime slime in _slimes)
        {
            slime.Initialize(_slimeMovementConfig);

            yield return null;
        }
    }
}
