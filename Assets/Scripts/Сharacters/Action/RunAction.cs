using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContactCheck))]
public class RunAction : MonoBehaviour, ICharacterAction
{
    public enum FaceDirection
    {
        Right,
        Left
    }

    private static readonly int HorizontalSpeed = Animator.StringToHash("hSpeed");

    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private ContactCheck _contactCheck;

    private FaceDirection _faceDirection;
    private Vector2 _desiredVelocity;
    private float _maxSpeedChange;

    public FaceDirection Direction => _faceDirection;

    private void Awake()
    {
        _faceDirection = FaceDirection.Right;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _contactCheck = GetComponent<ContactCheck>();
    }

    private void Start()
    {
        var actorController = GetComponent<CharacterController>();
        actorController.Register(this);
        actorController.OnMoveInput += OnMoveInputHandler;
    }


    private void OnMoveInputHandler(float input)
    {
        _desiredVelocity = new Vector2(input, 0f) * Mathf.Max(_maxSpeed - 0.5f, 0f);

        _faceDirection = input switch
        {
            > 0 => FaceDirection.Right,
            < 0 => FaceDirection.Left,
            _ => _faceDirection
        };

        _renderer.flipX = _faceDirection != FaceDirection.Right;
    }

    public void Run()
    {
        var onGround = _contactCheck.IsGrounded;
        var acceleration = onGround ? _maxAcceleration : _maxAirAcceleration;

        var velocity = _rigidbody.velocity;
        var maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, _desiredVelocity.x, maxSpeedChange);

        _rigidbody.velocity = velocity;
        _animator.SetFloat(HorizontalSpeed, Mathf.Abs(velocity.x));
    }
}
