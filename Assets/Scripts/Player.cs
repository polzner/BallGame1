using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 1;

    public event UnityAction Dead;
    public event UnityAction<int> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Dead?.Invoke();

        HealthChanged?.Invoke(_health);
    }
}
