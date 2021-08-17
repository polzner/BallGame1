using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _groundPart;
    [SerializeField] private GameObject _spike;
    [SerializeField] private GameObject _coin;
    [SerializeField] private int _groundPartQuantity;
    [SerializeField] private int _spikeQuantity;
    [SerializeField] private int _coinQuantity;

    private List<GameObject> _groundPool = new List<GameObject>();
    private List<GameObject> _spikePool = new List<GameObject>();
    private List<GameObject> _coinPool = new List<GameObject>();    

    private void Start()
    {
        FillContainer(_groundPool, _groundPart, _groundPartQuantity);
        FillContainer(_spikePool, _spike, _spikeQuantity);
        FillContainer(_coinPool, _coin, _coinQuantity);
    }

    private void FillContainer(List<GameObject> pool, GameObject prefab, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject clone = Instantiate(prefab, _container);
            clone.SetActive(false);
            pool.Add(clone);
        }
    }

    public bool TryGetObjectWithRandomChance(out GridObject part, GridObjectType gridObjectType)
    {
        part = null;

        switch (gridObjectType)
        {
            case GridObjectType.Ground:                
                return TryGetObjectInPoolWithRandomChance(out part, _groundPool);
            case GridObjectType.Spike:
                return TryGetObjectInPoolWithRandomChance(out part, _spikePool);
            case GridObjectType.Coin:
                return TryGetObjectInPoolWithRandomChance(out part, _coinPool);
        }

        return false;
    }

    public bool TryGetObject(out GridObject part, GridObjectType gridObjectType)
    {
        part = null;

        switch (gridObjectType)
        {
            case GridObjectType.Ground:
                return TryGetObjectInPool(out part, _groundPool);
            case GridObjectType.Spike:
                return TryGetObjectInPool(out part, _spikePool);
            case GridObjectType.Coin:
                return TryGetObjectInPool(out part, _coinPool);
        }

        return false;
    }

    private bool TryGetObjectInPoolWithRandomChance(out GridObject part, List<GameObject> pool)
    {
        part = null;

        foreach (var item in pool)
        {
            if (!item.activeSelf)
            {
                GridObject gridObject = item.GetComponent<GridObject>();

                if (gridObject.Chance > UnityEngine.Random.Range(0f, 100f))
                {
                    part = gridObject;
                    return true;
                }
            }
        }

        return false;
    }

    private bool TryGetObjectInPool(out GridObject part, List<GameObject> pool)
    {
        part = null;

        foreach (var item in pool)
        {
            if (!item.activeSelf)
            {
                GridObject gridObject = item.GetComponent<GridObject>();
                part = gridObject;
                return true;
            }
        }

        return false;
    }

    public void DeselectOutOfScreen()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        foreach (var item in _groundPool)
        {
            if (item.gameObject.activeSelf && item.transform.position.x < pointOutOfScreen.x - 1)
            {
                item.gameObject.SetActive(false);
                item.transform.position = _container.position;
            }
        }
    }   
}