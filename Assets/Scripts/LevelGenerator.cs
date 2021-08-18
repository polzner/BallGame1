using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PoolsContainer _pool;
    [SerializeField] private WorldGrid _grid;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private float _radius;
    [SerializeField] private int _minCoinInLine;
    [SerializeField] private int _maxCoinInLine;    

    private void Update()
    {
        CreateParts(_playerTransform.position, _radius);
    }

    private void CreateParts(Vector3 center, float radius)
    {
        int cellQuantity = (int)(radius / _cellSize);

        for (int x = -cellQuantity; x < cellQuantity; x++)
        {
            var gridPosition = _grid.WorldToGrid(center, _cellSize) + new Vector2Int(x, 0);
            TryPlacePart(_grid.WorldToGrid(center, _cellSize) + new Vector2Int(x, 0), GridLevel.Ground, GridObjectType.Ground);

            if (x >= 0)
            {
                TryPlacePart(_grid.WorldToGrid(center, _cellSize) + new Vector2Int(x, 0), GridLevel.OnGround, GridObjectType.Spike);
                TryPlacePartInLine(_grid.WorldToGrid(center, _cellSize) + new Vector2Int(x, 0),
                    GridLevel.OnGround, UnityEngine.Random.Range(_minCoinInLine, _maxCoinInLine), GridObjectType.Coin);
            }

            _grid.AddCollision(gridPosition);
        }
    }

    private void TryPlacePart(Vector2Int gridPosition, GridLevel level, GridObjectType type)
    {
        if (_grid.Contains(gridPosition))
            return;

        gridPosition.y = (int)level;

        if (_pool.TryGetObjectWithRandomChance(out GridObject part, type))
        {
            _grid.AddCollision(gridPosition);
            part.transform.position = _grid.GridToWorld(gridPosition, _cellSize);
            part.gameObject.SetActive(true);
        }
    }

    private void TryPlacePartInLine(Vector2Int gridPosition, GridLevel level, int objectsInLineQuantity, GridObjectType type)
    {
        if (_grid.Contains(gridPosition))
            return;

        gridPosition.y = (int)level;

        for (int i = 0; i < objectsInLineQuantity; i++)
        {
            gridPosition.x += (int)_cellSize;

            if (!_grid.Contains(gridPosition) && _pool.TryGetObjectWithRandomChance(out GridObject gridObject, type))
            {
                _grid.AddCollision(gridPosition);
                gridObject.transform.position = _grid.GridToWorld(gridPosition, _cellSize);
                gridObject.gameObject.SetActive(true);
            }
            else
            {
                return;
            }
        }
    }
}
