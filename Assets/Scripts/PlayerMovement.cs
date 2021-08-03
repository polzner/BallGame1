using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _jumpForce = 100;
    [SerializeField] private AnimationCurve _normalizedSpeedByDistance;

    private bool _grounded;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_maxSpeed * _normalizedSpeedByDistance.Evaluate(transform.position.x), _rigidBody.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _grounded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _grounded = false;
        }
    }
}
