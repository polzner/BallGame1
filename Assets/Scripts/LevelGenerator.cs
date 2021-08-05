using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private float _radius;
    [SerializeField] private int _minCoinInLine;
    [SerializeField] private int _maxCoinInLine;

    private List<Vector2Int> _collisionMatrix = new List<Vector2Int>();   
    
    private void Update()
    {
        int cellQuantity = (int)(_radius / _cellSize);

        for (int x = -cellQuantity; x < cellQuantity; x++)
        {
            _pool.DeselectOutOfScreen();
            TryPlacePart(WorldToGrid(_playerTransform.position) + new Vector2Int(x, 0), GridLevel.Ground, 
                (gridObject) => gridObject.TryGetComponent<Ground>(out Ground ground));

            if (x >= 0 && x % 2 == 0)
            {
                TryPlacePart(WorldToGrid(_playerTransform.position) + new Vector2Int(x, 0), GridLevel.OnGround,
                    (gridObject) => gridObject.TryGetComponent<Spike>(out Spike spike));
            }

            if(x >= 0)
            {            
                TryPlacePartInLine(WorldToGrid(_playerTransform.position) + new Vector2Int(x, 0), GridLevel.OnGround, Random.Range(_minCoinInLine, _maxCoinInLine),
                    (gridObject) => gridObject.TryGetComponent<Coin>(out Coin coin));
            }

            var gridPosition = WorldToGrid(_playerTransform.position) + new Vector2Int(x, 0);

            if (!_collisionMatrix.Contains(gridPosition))
            {
                _collisionMatrix.Add(gridPosition);
            }
        }

        CleanCollisionMatrix();
    }

    private void TryPlacePart(Vector2Int gridPosition, GridLevel level, Func<GameObject, bool> condition)
    {
        gridPosition.y = (int)level;

        if (_collisionMatrix.Contains(gridPosition))
            return;

        if (_pool.TryGetRandomObject(level, out GridObject part, condition))
        {
            _collisionMatrix.Add(gridPosition);
            part.transform.position = GridToWorld(gridPosition);
            part.gameObject.SetActive(true);
        }
    }

    private void TryPlacePartInLine(Vector2Int gridPosition, GridLevel level, int objectsInLineQuantity, Func<GameObject, bool> condition)
    {
        gridPosition.y = (int)level;

        if (_collisionMatrix.Contains(gridPosition))
            return;

        if (_pool.TryGetRandomObject(level, out GridObject part, condition))
        {
            for (int i = 0; i < objectsInLineQuantity; i++)
            {
                gridPosition.x += (int)_cellSize;

                if (!_collisionMatrix.Contains(gridPosition))
                {
                    if (_pool.TryGetObject(level, out GridObject gridObject, condition))
                    {
                        _collisionMatrix.Add(gridPosition);
                        gridObject.transform.position = GridToWorld(gridPosition);
                        gridObject.gameObject.SetActive(true);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void CleanCollisionMatrix()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        for (int i = 0; i < _collisionMatrix.Count; i++)
        {
            if (_collisionMatrix[i].x < pointOutOfScreen.x)
                _collisionMatrix.Remove(_collisionMatrix[i]);
        }
    }

    private Vector2 GridToWorld(Vector2Int position)
    {
        Vector2 worldPosition = Vector2.zero;
        worldPosition.x = position.x * _cellSize;
        worldPosition.y = position.y * _cellSize;
        return worldPosition;
    }

    private Vector2Int WorldToGrid(Vector2 position)
    {
        Vector2Int gridPosition = Vector2Int.zero;
        gridPosition.x = (int)(position.x / _cellSize);
        gridPosition.y = (int)(position.y / _cellSize);
        return gridPosition;
    }
}
