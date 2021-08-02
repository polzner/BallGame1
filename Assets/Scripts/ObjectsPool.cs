using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _groundPart;
    [SerializeField] private int _partQuantity;

    private List<GameObject> _partsPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _partQuantity; i++)
        {
            GameObject part = Instantiate(_groundPart, _container);
            part.SetActive(false);
            _partsPool.Add(part);
        }
    }

    public bool TryGetPart(out GameObject part)
    {
        part = null;
        part = _partsPool.FirstOrDefault(part => !part.activeSelf);
        return part != null;
    }

    public void DeselectOutOfScreen()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        foreach (var item in _partsPool)
        {
            if (item.gameObject.activeSelf && item.transform.position.x < pointOutOfScreen.x - 1)
            {
                item.gameObject.SetActive(false);
                item.transform.position = _container.position;
            }
        }
    }
}
