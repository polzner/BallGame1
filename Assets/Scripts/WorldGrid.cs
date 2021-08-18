using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField] private float _timeToCleanCollisionMatrixList;
    [SerializeField] private Camera _camera;
    private List<Vector2Int> _collisionMatrix = new List<Vector2Int>();
    private float _timestamp;

    private void Update()
    {
        if (Time.time - _timestamp >= _timeToCleanCollisionMatrixList)
        {
            _timestamp = Time.time;
            CleanCollisionMatrix();
        }
    }

    private void CleanCollisionMatrix()
    {
        Vector3 pointOutOfScreen = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        for (int i = 0; i < _collisionMatrix.Count; i++)
        {
            if (_collisionMatrix[i].x < pointOutOfScreen.x)
            {
                _collisionMatrix.Remove(_collisionMatrix[i]);
            }
        }
    }

    public void AddCollision(Vector2Int gridPosition)
    {
        if (!_collisionMatrix.Contains(gridPosition))
        {
            _collisionMatrix.Add(gridPosition);
        }
    }

    public bool Contains(Vector2Int position)
    {
        return _collisionMatrix.Contains(position);
    }

    public Vector2 GridToWorld(Vector2Int position, float cellSize)
    {
        Vector2 worldPosition = Vector2.zero;
        worldPosition.x = position.x * cellSize;
        worldPosition.y = position.y * cellSize;
        return worldPosition;
    }

    public Vector2Int WorldToGrid(Vector2 position, float cellSize)
    {
        Vector2Int gridPosition = Vector2Int.zero;
        gridPosition.x = (int)(position.x / cellSize);
        gridPosition.y = (int)(position.y / cellSize);
        return gridPosition;
    }
}
