using UnityEngine;

[RequireComponent(typeof(ContactCheck))]
public class RunAction : BaseMoveAction
{
    private static readonly int HorizontalSpeed = Animator.StringToHash("hSpeed");

    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ContactCheck _contactCheck;
    private SpriteRenderer _renderer;
    private Vector2 _desiredVelocity;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _contactCheck = GetComponent<ContactCheck>();
    }

    private void Start()
    {
        var actorController = GetComponent<CharacterController>();
        actorController.Register(this);
        actorController.OnMoveInput += OnMoveInputHandler;
    }
    protected override void OnMoveInputHandler(float input)
    {
        if (input > 0) input = 1;
        if (input < 0) input = -1;
        _desiredVelocity = new Vector2(input, 0f) * Mathf.Max(_maxSpeed, 0f);
        FaceFlipDirection(_renderer, input);
    }

    public override void Run()
    {
        var velocity = _rigidbody.velocity;

        if (_desiredVelocity.x > 0.1 || _desiredVelocity.x < 0.1)
        {
            var onGround = _contactCheck.IsGrounded;
            var acceleration = onGround ? _maxAcceleration : _maxAirAcceleration;

            var maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, _desiredVelocity.x, maxSpeedChange);
        }

        _rigidbody.velocity = velocity;
        _animator.SetFloat(HorizontalSpeed, Mathf.Abs(velocity.x));
    }
}
