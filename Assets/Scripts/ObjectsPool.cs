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
    [SerializeField] private int _groundPartQuantity;
    [SerializeField] private int _spikeQuantity;

    private List<GameObject> _objectsPool = new List<GameObject>();

    private void Start()
    {
        FillContainer(_groundPart, _groundPartQuantity);
        FillContainer(_spike, _spikeQuantity);
    }

    private void FillContainer(GameObject prefab, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject clone = Instantiate(prefab, _container);
            clone.SetActive(false);
            _objectsPool.Add(clone);
        }
    }

    public bool TryGetRandomObject(GridLevel level, out GridObject prefab)
    {
        prefab = null;
        List<GridObject> possiblePrefabs = new List<GridObject>();
        foreach (var gridObject in _objectsPool)
        {
            if(gridObject.GetComponent<GridObject>().Level == level && !gridObject.activeSelf)
            {
                possiblePrefabs.Add(gridObject.GetComponent<GridObject>());
            }
        }

        foreach (var possiblePrefab in possiblePrefabs)
        {
            if (possiblePrefab.Chance > Random.Range(0, 1000))
            {
                prefab = possiblePrefab;
                return true;
            }
        }

        return false;
    }

    public void DeselectOutOfScreen()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        foreach (var item in _objectsPool)
        {
            if (item.gameObject.activeSelf && item.transform.position.x < pointOutOfScreen.x - 1)
            {
                item.gameObject.SetActive(false);
                item.transform.position = _container.position;
            }
        }
    }
}
