using System;
using UnityEngine;

public class BagHandler
{
    private readonly BagConfig _bagConfig;
    private readonly GameObject _spikesTilemap;
    private int _currentKeysCount = 0;
    private int _remainingKeysCount;

    public int RemainingKeysCount => _remainingKeysCount;

    public BagHandler(BagConfig bagConfig, GameObject spikesTilemap)
    {
        _bagConfig = bagConfig;
        _spikesTilemap = spikesTilemap;
        _remainingKeysCount = _bagConfig.KeysCount;
    }

    public bool IsEnoughKeys()
    {
        if(_currentKeysCount < _bagConfig.KeysCount)
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
