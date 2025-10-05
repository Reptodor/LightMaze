using System;
using UnityEngine;

public class BagHandler
{
    private readonly GameObject _spikesTilemap;
    private readonly int _keysCount;
    private int _currentKeysCount = 0;
    private int _remainingKeysCount;

    public int RemainingKeysCount => _remainingKeysCount;

    public BagHandler(int keysCount, GameObject spikesTilemap)
    {
        _keysCount = keysCount;
        _spikesTilemap = spikesTilemap;
        _remainingKeysCount = _keysCount;
    }

    public bool IsEnoughKeys()
    {
        if(_currentKeysCount < _keysCount)
            return false;

        return true;
    }

    public void AddKey(int keysCount)
    {
        if(keysCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(keysCount), "Keys count must be greater than zero");

        _currentKeysCount += keysCount;
        _remainingKeysCount -= keysCount;

        if(_remainingKeysCount == 0)
            _spikesTilemap.SetActive(false);
    }
}
