using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerJump _jump;

    private State _state;

    private void OnEnable()
    {
        _jump.JumpEnd += OnJumpEnd;
    }

    private void OnDisable()
    {
        _jump.JumpEnd -= OnJumpEnd;

    }

    private enum State
    {
        Moving,
        Jumping
    }

    private void FixedUpdate()
    {
        if (_state == State.Moving)
        {
            _move.Move();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _state == State.Moving)
        {
            _state = State.Jumping;
            _jump.PlayAction();
        }
    }

    private void OnJumpEnd()
    {
        _state = State.Moving;
    }
}
