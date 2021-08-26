using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLevel _level;
    [SerializeField] [Range(0,100)] private float _chance;
    [SerializeField] private int _minObjectsInLine;
    [SerializeField] private int _maxObjectsInLine;

    public GridLevel Level => _level;
    public float Chance => _chance;
    public int MinObjectsInLine => _minObjectsInLine;
    public int MaxObjectsInLine => _maxObjectsInLine;
}
