using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;

    private Vector2 _normal = Vector2.zero;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _normal = collision.contacts[0].normal;
    }

    public void Move()
    {
        if (_normal != Vector2.zero)
        {
            Vector2 correctNormal = new Vector2(_normal.y, _normal.x);
            var offset = Vector2.right * Vector3.Dot(Vector3.right, correctNormal) * _moveSpeed * Time.deltaTime;
            _rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + offset);
        }
    }
}
