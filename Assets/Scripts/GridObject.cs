using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLevel _level;
    [SerializeField] private int _chance;

    public GridLevel Level => _level;
    public int Chance => _chance;
}
