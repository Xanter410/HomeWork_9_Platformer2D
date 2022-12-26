using UnityEngine;

[RequireComponent(typeof(ContactCheck))]
public class JumpAction : MonoBehaviour, ICharacterAction
{
    private static readonly int VerticalSpeed = Animator.StringToHash("vSpeed");
    private static readonly int Grounded = Animator.StringToHash("grounded");

    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private ContactCheck _contact;

    private Vector2 _velocity;
    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;
    private bool _desiredJump, _onGround;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _contact = GetComponent<ContactCheck>();
        _defaultGravityScale = 1f;
    }

    private void Start()
    {
        var actorController = GetComponent<CharacterController>();
        actorController.Register(this);
        actorController.OnJumpInput += OnJumpInputHandler;
    }

    private void OnJumpInputHandler(bool input)
    {
        _desiredJump |= input;
    }

    public void Run()
    {
        _onGround = _contact.IsGrounded;
        _velocity = _rigidbody.velocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            Jump();
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

    private void Jump()
    {
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_rigidbody.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }
}