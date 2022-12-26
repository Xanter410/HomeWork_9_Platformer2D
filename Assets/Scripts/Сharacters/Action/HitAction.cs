using UnityEngine;

[RequireComponent(typeof(RunAction))]
public class HitAction : MonoBehaviour, ICharacterAction
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private Transform _rightHitSpawnPoint;
    [SerializeField] private Transform _leftHitSpawnPoint;
    [SerializeField, Range(0f, 10f)] private float _firingInterval;
    [SerializeField, Range(0f, 10f)] private float _comboInterval;
    private float _previousHitTime;
    private RunAction _runAction;
    private Animator _animator;
    private bool _desiredHit;
    private int _comboHit;

    private void Awake()
    {
        _runAction = GetComponent<RunAction>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        var characterController = GetComponent<CharacterController>();
        characterController.Register(this);
        characterController.OnHitImput += OnFireInputHandler;
    }

    private void OnFireInputHandler(bool input)
    {
        _desiredHit |= input;
    }

    public void Run()
    {
        if (_desiredHit)
        {
            _desiredHit = false;
            if (_previousHitTime + _firingInterval < Time.time)
            {
                if (_previousHitTime + _comboInterval > Time.time)
                {
                    _comboHit++;
                }
                else
                {
                    _comboHit = 1;
                }

                _animator.SetInteger(_attacked, _comboHit);
                _previousHitTime = Time.time;
                Hit();
            }
        }
    }

    private void Hit()
    {
        var hitSpawnPoint = _runAction.Direction == RunAction.FaceDirection.Right
            ? _rightHitSpawnPoint.position
            : _leftHitSpawnPoint.position;

        //var hitGameObject = Instantiate(_hitPrefab, hitSpawnPoint, Quaternion.identity);
    }
}
