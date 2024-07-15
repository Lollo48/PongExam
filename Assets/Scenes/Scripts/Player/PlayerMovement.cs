using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInputManager _playerInput;
    [SerializeField] private float _speed;
    Rigidbody _rigidbody;

    public static event Action OnMove;
    
    private void Awake()
    {
        _playerInput = new PlayerInputManager(this);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput.EnabledInput();
    }

    private void OnDisable()
    {
        _playerInput.DisabledInput();
    }

    private void FixedUpdate()
    {
        OnMove?.Invoke();
    }

    public void Move()
    {
        float input = _playerInput._characterInput.Movement.Move.ReadValue<float>();
        Vector2 direction = new Vector2(0f, input);
        direction *= _speed * 1000 * Time.deltaTime;
        _rigidbody.velocity = direction;

    }


}
