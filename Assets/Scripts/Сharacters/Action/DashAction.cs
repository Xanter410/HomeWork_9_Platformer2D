using UnityEngine;

public class DashAction : MonoBehaviour, ICharacterAction
{
    private static readonly int dashAnimator = Animator.StringToHash("dash");

    [SerializeField, Range(0f, 10f)] private float _dashInterval;
    [SerializeField, Range(0f, 10f)] private float _waitTimeBeforeDash;
    [SerializeField, Range(0f, 10f)] private float _maxDashDistance = 5f;
    [SerializeField] private float _maxDashSpeed = 50f;

    private BaseMoveAction _moveAction;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private float _previousDashTime;
    private float _previousWaitTime;
    private bool _desiredHit;
    private bool _permissionHit;

    private void Awake()
    {
        _moveAction = GetComponent<BaseMoveAction>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var enemyController = GetComponent<EnemyController>();
        enemyController.Register(this);
        enemyController.OnDashInput += OnDashInputHandler;
    }

    private void OnDashInputHandler(bool input)
    {
        _desiredHit = input;
    }

    public void Run()
    {
        if (_desiredHit && !_permissionHit)
        {
            _desiredHit = false;
            if (_previousDashTime + _dashInterval < Time.time)
            {
                _previousWaitTime = Time.time;
                _permissionHit = true;

                _animator.SetBool(dashAnimator, true);
            }
        }

        if (_permissionHit && _previousWaitTime + _waitTimeBeforeDash < Time.time)
        {
            _previousDashTime = Time.time;
            _permissionHit = false;

            Dash();
        }
    }

    private void Dash()
    {
        var desiredVelocity = new Vector2(_maxDashDistance * 2, 0f);
        desiredVelocity.x *= _moveAction.Direction == FaceDirection.Right ? 1 : -1;

        var velocity = _rigidbody.velocity;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, _maxDashSpeed);
        _rigidbody.velocity = velocity;
    }
}
