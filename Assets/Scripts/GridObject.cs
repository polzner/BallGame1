using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLevel _level;
    [SerializeField] [Range(0,100)] private float _chance;

    public GridLevel Level => _level;
    public float Chance => _chance;
}
