using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _timeToCleanCollisionMatrixList;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private float _radius;
    [SerializeField] private int _minCoinInLine;
    [SerializeField] private int _maxCoinInLine;    

    private List<Vector2Int> _collisionMatrix = new List<Vector2Int>();
    private float _timestamp;
    
    private void Update()
    {
        CreateParts(_playerTransform.position, _radius);

        if(Time.time - _timestamp >= _timeToCleanCollisionMatrixList)
        {
            _timestamp = Time.time;
            CleanCollisionMatrix();
        }
    }

    private void CreateParts(Vector3 center, float radius)
    {
        int cellQuantity = (int)(radius / _cellSize);

        for (int x = -cellQuantity; x < cellQuantity; x++)
        {
            var gridPosition = WorldToGrid(center) + new Vector2Int(x, 0);
            _pool.DeselectOutOfScreen();
            TryPlacePart(WorldToGrid(center) + new Vector2Int(x, 0), GridLevel.Ground, GridObjectType.Ground);

            if (x >= 0 && x % 2 == 0)
            {
                TryPlacePart(WorldToGrid(center) + new Vector2Int(x, 0), GridLevel.OnGround, GridObjectType.Spike);
            }

            if (x >= 0)
            {
                TryPlacePartInLine(WorldToGrid(center) + new Vector2Int(x, 0),
                    GridLevel.OnGround, UnityEngine.Random.Range(_minCoinInLine, _maxCoinInLine), GridObjectType.Coin);
            }

            if (!_collisionMatrix.Contains(gridPosition))
            {
                _collisionMatrix.Add(gridPosition);
            }
        }
    }

    private void TryPlacePart(Vector2Int gridPosition, GridLevel level, GridObjectType type)
    {
        gridPosition.y = (int)level;

        if (_collisionMatrix.Contains(gridPosition))
            return;

        if (_pool.TryGetObjectWithRandomChance(out GridObject part, type))
        {
            _collisionMatrix.Add(gridPosition);
            part.transform.position = GridToWorld(gridPosition);
            part.gameObject.SetActive(true);
        }
    }

    private void TryPlacePartInLine(Vector2Int gridPosition, GridLevel level, int objectsInLineQuantity, GridObjectType type)
    {
        gridPosition.y = (int)level;

        if (_collisionMatrix.Contains(gridPosition))
            return;

        if (_pool.TryGetObjectWithRandomChance(out GridObject part, type))
        {
            for (int i = 0; i < objectsInLineQuantity; i++)
            {
                gridPosition.x += (int)_cellSize;

                if (!_collisionMatrix.Contains(gridPosition) && _pool.TryGetObject(out GridObject gridObject, type))
                {
                    _collisionMatrix.Add(gridPosition);
                    gridObject.transform.position = GridToWorld(gridPosition);
                    gridObject.gameObject.SetActive(true);
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
