using System.Diagnostics.Contracts;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class PatrolBehaviour : Behavior
{
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private float _idleIntervalOnBound = 4f;

    private EnemyController _enemyController;
    private float _previousIdleTimeOnBound;
    private bool _isIdle;
    private int _boundIndex;
    float boundPosition;
    float boundDirection;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    public override bool Evaluate()
    {

        if (_isIdle && _previousIdleTimeOnBound + _idleIntervalOnBound > Time.time)
        {
            return false;
        }
        else
        {
            _isIdle = false;
            return true;
        }
    }

    public override void Execute()
    {
        boundPosition = _boundIndex == 0 ? _leftBound.position.x : _rightBound.position.x;
        boundDirection = boundPosition - transform.position.x;

        if (Mathf.Abs(boundDirection) < 0.5)
        {
            _boundIndex = _boundIndex == 0 ? 1 : 0;

            _isIdle = true;
            _previousIdleTimeOnBound = Time.time;

            _enemyController.OnMoveInputHandler(0);
            return;
        }

        if (boundDirection >= 0)
        {
            _enemyController.OnMoveInputHandler(1);
        }
        else if (boundDirection < 0)
        {
            _enemyController.OnMoveInputHandler(-1);
        }
    }
}
