using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpTragectory;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _jumpRange;
    [SerializeField] private float _jumpHeigh;

    private Vector2 _normal = Vector2.zero;
    private Rigidbody2D _rigidBody;
    private float _currentValue;
    private State _state;

    private enum State
    {
        Moving,
        Jumping
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_state == State.Moving && _normal != Vector2.zero)
        {
            Vector2 correctNormal = new Vector2(_normal.y, _normal.x);
            var offset = Vector2.right * Vector3.Dot(Vector3.right, correctNormal) * _moveSpeed * Time.deltaTime;
            _rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + offset);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _normal = collision.contacts[0].normal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _state == State.Moving)
        {
            _state = State.Jumping;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        var delay = new WaitForFixedUpdate();
        float offset = 0;
        Vector3 startPosition = transform.position;
        float lastCurveKeyTime = _jumpTragectory.keys[_jumpTragectory.keys.Length - 1].time;

        while (_currentValue <= lastCurveKeyTime)
        {
            offset += _jumpRange * Time.deltaTime;
            _currentValue += Time.deltaTime;
            _rigidBody.MovePosition(startPosition + new Vector3(offset, _jumpTragectory.Evaluate(_currentValue) * _jumpHeigh, 0));
            yield return delay;
        }

        _currentValue = 0;
        _state = State.Moving;
    }
}
