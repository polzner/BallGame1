using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsContainer : MonoBehaviour
{
    [SerializeField] private Pool _groundPool;
    [SerializeField] private Pool _spikePool;
    [SerializeField] private Pool _coinPool;

    public bool TryGetObjectWithRandomChance(out GridObject gridObject, GridObjectType type)
    {        
        gridObject = null;

        switch (type)
        {
            case GridObjectType.Ground:
                return _groundPool.TryGetObjectWithRandomChance(out gridObject);
            case GridObjectType.Spike:
                return _spikePool.TryGetObjectWithRandomChance(out gridObject);
            case GridObjectType.Coin:
                return _coinPool.TryGetObjectWithRandomChance(out gridObject);
        }

        return false;
    }
}