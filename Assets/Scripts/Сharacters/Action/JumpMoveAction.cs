using UnityEngine;
using static RunAction;

[RequireComponent(typeof(ContactCheck))]
public class JumpMoveAction : MonoBehaviour, ICharacterAction
{
    private static readonly int VerticalSpeed = Animator.StringToHash("vSpeed");
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private static readonly int JumpedAnimator = Animator.StringToHash("jumped");

    [SerializeField, Range(0f, 10f)] private float _maxJumpDistance = 3f;
    [SerializeField] private float _maxSpeed = 999f;
    [SerializeField, Range(0f, 5f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 2f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 2f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private ContactCheck _contact;
    private FaceDirection _faceDirection;
    private SpriteRenderer _renderer;

    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private float _defaultGravityScale, _jumpSpeed;
    private bool _desiredJump, _onGround;

    public FaceDirection Direction => _faceDirection;

    private void Awake()
    {
        _faceDirection = FaceDirection.Right;
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _contact = GetComponent<ContactCheck>();
        _defaultGravityScale = 1f;
    }

    private void Start()
    {
        var characterController = GetComponent<CharacterController>();
        characterController.Register(this);
        characterController.OnMoveInput += OnMoveInputHandler;
    }

    private void OnMoveInputHandler(float input)
    {
        _desiredVelocity = new Vector2(input, 0f) * Mathf.Max(_maxJumpDistance, 0f);

        _faceDirection = input switch
        {
            > 0 => FaceDirection.Right,
            < 0 => FaceDirection.Left,
            _ => _faceDirection
        };

        _renderer.flipX = _faceDirection != FaceDirection.Right;

        _desiredJump = true;
    }

    public void Run()
    {
        _onGround = _contact.IsGrounded;
        _velocity = _rigidbody.velocity;

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpMove();
            _animator.SetBool(JumpedAnimator, true);
        }

        if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.gravityScale = _upwardMovementMultiplier;
        }
        else if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _downwardMovementMultiplier;
        }
        else
        {
            _rigidbody.gravityScale = _defaultGravityScale;
        }

        _rigidbody.velocity = _velocity;

        _animator.SetFloat(VerticalSpeed, _velocity.y);
        _animator.SetBool(Grounded, _onGround);
    }

    private void JumpMove()
    {
        if (_onGround)
        {
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            _velocity.y += _jumpSpeed;

            var maxSpeedChange = _maxSpeed * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
        }
    }
}
