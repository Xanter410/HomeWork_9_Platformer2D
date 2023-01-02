using UnityEngine;

[RequireComponent(typeof(ContactCheck))]
public class JumpMoveAction : BaseMoveAction
{
    private static readonly int VerticalSpeed = Animator.StringToHash("vSpeed");
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private static readonly int JumpedAnimator = Animator.StringToHash("jumped");

    [SerializeField] private float _maxSpeed = 999f;
    [SerializeField, Range(0f, 5f)] private float _maxJumpDistant = 3f;
    [SerializeField, Range(0f, 5f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 2f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 2f;
    [SerializeField] private float _idleIntervalBetweenJumps = 2f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ContactCheck _contact;
    private SpriteRenderer _renderer;

    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private float _defaultGravityScale, _jumpSpeed;
    private bool _desiredJump, _onGround;
    private float _previousIdleTimeBetweenJumps;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _contact = GetComponent<ContactCheck>();
        _defaultGravityScale = 1f;
    }

    private void Start()
    {
        var characterController = GetComponent<CharacterController>();
        characterController.Register(this);
        characterController.OnMoveInput += OnMoveInputHandler;
    }

    protected override void OnMoveInputHandler(float input)
    {
        if (Mathf.Abs(input) > 0.1)
        {
            _desiredVelocity = new Vector2(input, 0f) * Mathf.Max(_maxJumpDistant, 0f);
            FaceFlipDirection(_renderer, input);

            _desiredJump = true;
        }
    }

    public override void Run()
    {
        _onGround = _contact.IsGrounded;
        _velocity = _rigidbody.velocity;

        if (_desiredJump)
        {
            _desiredJump = false;

            if (_previousIdleTimeBetweenJumps + _idleIntervalBetweenJumps < Time.time)
            {
                JumpMove();
            }
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

        _animator.SetFloat(VerticalSpeed, _velocity.y);
        _animator.SetBool(Grounded, _onGround);
    }

    private void JumpMove()
    {
        if (_onGround)
        {
            var velocity = _velocity;

            _animator.SetBool(JumpedAnimator, true);

            _previousIdleTimeBetweenJumps = Time.time;

            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            velocity.y = _jumpSpeed;

            var maxSpeedChange = _maxSpeed * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);

            _rigidbody.velocity = velocity;
        }
    }
}
