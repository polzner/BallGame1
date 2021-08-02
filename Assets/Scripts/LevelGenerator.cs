using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private float _radius;

    private List<Vector2Int> _collisionMatrix = new List<Vector2Int>();
    private enum GridLevel
    {
        Ground = 0,
        Trap = 1
    }

    private void Update()
    {
        int cellQuantity = (int)(_radius / _cellSize);

        for (int x = -cellQuantity; x < cellQuantity; x++)
        {
            _pool.DeselectOutOfScreen();
            TryPlacePart(WorldToGrid(_playerTransform.position) + new Vector2Int(x, 0), GridLevel.Ground);
        }
    }

    private void TryPlacePart(Vector2Int gridPosition, GridLevel level)
    {
        if (_collisionMatrix.Contains(gridPosition))
            return;

        if (_pool.TryGetPart(out GameObject part))
        {
            _collisionMatrix.Add(gridPosition);
            gridPosition.y = (int)level;
            part.transform.position = GridToWorld(gridPosition);
            part.gameObject.SetActive(true);
        }
    }

    public Vector2 GridToWorld(Vector2Int position)
    {
        Vector2 worldPosition = Vector2.zero;
        worldPosition.x = position.x * _cellSize;
        worldPosition.y = position.y * _cellSize;
        return worldPosition;
    }

    public Vector2Int WorldToGrid(Vector2 position)
    {
        Vector2Int gridPosition = Vector2Int.zero;
        gridPosition.x = (int)(position.x / _cellSize);
        gridPosition.y = (int)(position.y / _cellSize);
        return gridPosition;
    }
}
