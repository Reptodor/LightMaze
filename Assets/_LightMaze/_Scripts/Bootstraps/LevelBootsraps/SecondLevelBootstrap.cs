using System.Collections;
using UnityEngine;

public class SecondLevelBootstrap : FirtsLevelBootstrap
{
    [Header("Keys")]
    [SerializeField] private Key[] _keys;
    [SerializeField] private KeyMapHandler _keyMapHandler;

    public override IEnumerator Initialize()
    {
        yield return base.Initialize();

        yield return null;

        _keyMapHandler.Initialize(_keys);
    }
}
