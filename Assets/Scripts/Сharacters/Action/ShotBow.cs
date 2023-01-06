using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ShotBow : MonoBehaviour, ICharacterAction
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    [SerializeField, Range(0f, 10f)] private float _waitTimeBeforeShot;
    [SerializeField, Range(0f, 10f)] private float _firingInterval;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private Transform _leftShotSpawnPoint;
    [SerializeField] private Transform _rightShotSpawnPoint;
    [SerializeField, Range(0f, 20f)] float _ForceShotX;
    [SerializeField, Range(0f, 10f)] float _ForceShotY;

    private BaseMoveAction _moveAction;
    private Animator _animator;
    private ContactCheck _contact;

    private float _previousWaitTime;
    private float _previousFiringTime;
    private bool _desiredShot;
    private bool _permissionShot;

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
        characterController.OnHitImput += OnShotInputHandler;
    }
    private void OnShotInputHandler(bool input)
    {
        _desiredShot = input;
    }

    public void Run()
    {
        if (_desiredShot && !_permissionShot)
        {
            _desiredShot = false;
            if (_previousFiringTime + _firingInterval < Time.time)
            {
                _previousWaitTime = Time.time;
                _permissionShot = true;

                _animator.SetBool(_attacked, true);
            }
        }

        if (_permissionShot && _previousWaitTime + _waitTimeBeforeShot < Time.time && _desiredShot)
        {
            _previousFiringTime = Time.time;
            _permissionShot = false;

            Shot();
        }
    }

    private void Shot()
    {
        var shotSpawnPoint = _moveAction.Direction == FaceDirection.Right
            ? _rightShotSpawnPoint.position
            : _leftShotSpawnPoint.position;

        var shotGameObject = Instantiate(_shotPrefab, shotSpawnPoint, Quaternion.identity);

        var shotDirection = _moveAction.Direction == FaceDirection.Right
                ? Vector3.right
                : Vector3.left;

        shotGameObject.GetComponent<Rigidbody2D>().AddForce(
            (Vector2)shotDirection * _ForceShotX + Vector2.up * _ForceShotY, 
            ForceMode2D.Impulse);
    }
}
