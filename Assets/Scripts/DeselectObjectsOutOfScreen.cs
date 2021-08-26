using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectObjectsOutOfScreen : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        DeselectOutOfScreen();
    }

    private void DeselectOutOfScreen()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        foreach (var item in _pool.Objects)
        {
            var gridObject = item as GameObject;
            if (gridObject.gameObject.activeSelf && gridObject.transform.position.x < pointOutOfScreen.x - 1)
            {
                gridObject.gameObject.SetActive(false);
                gridObject.transform.position = _pool.Container.position;
            }
        }
    }
}
