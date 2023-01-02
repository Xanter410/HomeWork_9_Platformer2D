using UnityEngine;

[RequireComponent(typeof(BaseMoveAction), typeof(ContactCheck))]
public class HitAction : MonoBehaviour, ICharacterAction
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    [SerializeField] private GameObject _rightHit;
    [SerializeField] private GameObject _leftHit;
    [SerializeField, Range(0f, 10f)] private float _firingInterval;

    private BaseMoveAction _moveAction;
    private Animator _animator;
    private ContactCheck _contact;

    private float _previousHitTime;
    private bool _desiredHit;

    private void Awake()
    {
        _moveAction = GetComponent<BaseMoveAction>();
        _animator = GetComponent<Animator>();
        _contact = GetComponent<ContactCheck>();
    }

    private void Start()
    {
        var characterController = GetComponent<CharacterController>();
        characterController.Register(this);
        characterController.OnHitImput += OnHitInputHandler;
    }

    private void OnHitInputHandler(bool input)
    {
        _desiredHit |= input;
    }

    public void Run()
    {
        if (_desiredHit)
        {
            _desiredHit = false;
            if (_contact.IsGrounded == true && _previousHitTime + _firingInterval < Time.time)
            {
                _animator.SetBool(_attacked, true);
                _previousHitTime = Time.time;
                Hit();
            }
        }
    }

    private void Hit()
    {
        var hitSpawnPoint = _moveAction.Direction == FaceDirection.Right
            ? _rightHit
            : _leftHit;

        hitSpawnPoint.SetActive(true);
    }
}
