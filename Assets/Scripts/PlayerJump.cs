using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpTragectory;
    [SerializeField] private float _jumpRange;
    [SerializeField] private float _jumpHeigh;

    private Rigidbody2D _rigidBody;

    public event UnityAction JumpEnd;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void PlayAction()
    {
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        var delay = new WaitForFixedUpdate();
        float offset = 0;
        float currentValue = 0;
        Vector3 startPosition = transform.position;
        float lastCurveKeyTime = _jumpTragectory.keys[_jumpTragectory.keys.Length - 1].time;

        while (currentValue <= lastCurveKeyTime)
        {
            offset += _jumpRange * Time.deltaTime;
            currentValue += Time.deltaTime;
            _rigidBody.MovePosition(startPosition + new Vector3(offset, _jumpTragectory.Evaluate(currentValue) * _jumpHeigh, 0));
            yield return delay;
        }

        JumpEnd.Invoke();
    }
}
