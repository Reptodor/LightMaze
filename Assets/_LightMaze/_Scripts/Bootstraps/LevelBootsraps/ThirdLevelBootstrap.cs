using System.Collections;
using UnityEngine;

public class ThirdLevelBootstrap : SecondLevelBootstrap
{
    [Header("Mines")]
    [SerializeField] private Mine[] _mines;
    [SerializeField] private Explosion _explosion;

    public override IEnumerator Initialize()
    {
        yield return base.Initialize();

        yield return null;

        foreach (Mine mine in _mines)
        {
            mine.Initialize(_explosion);
        }
    }
}
