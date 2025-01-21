using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBagConfig", menuName = "Configs/Bag")]
public class BagConfig : ScriptableObject
{
    [SerializeField] private int _keysCount;

    public int KeysCount => _keysCount;

    private void OnValidate()
    {
        if(_keysCount < 0)
            throw new ArgumentOutOfRangeException(nameof(_keysCount), "Keys count cannot be below zero");
    }
}
