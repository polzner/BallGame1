using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 1;

    public event UnityAction Dead;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Dead?.Invoke();
    }
}
