using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _gridObject;
    [SerializeField] private int _objectsQuantity;
    [SerializeField] private Transform _container;
    [SerializeField] private Camera _camera;

    private List<GameObject> _pool = new List<GameObject>();

    public Transform Container => _container;
    public IEnumerable Objects => _pool;

    private void Start()
    {
        FillContainer(_pool, _gridObject, _objectsQuantity);
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

    public bool TryGetObjectWithRandomChance(out GridObject gridObject)
    {
        gridObject = null;

        foreach (var item in _pool)
        {
            if (!item.activeSelf && item.GetComponent<GridObject>().Chance > Random.Range(0f, 100f))
            {
                gridObject = item.GetComponent<GridObject>();
            }
        }

        return gridObject != null;
    }
}
