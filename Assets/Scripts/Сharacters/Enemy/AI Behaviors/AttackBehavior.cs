using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : Behavior
{
    [SerializeField] private PlayerDetectZone _playerDetect;
    [SerializeField] private float _minAggressivenessTime;
    private float _lastAggressivenessTime;
    private EnemyController _enemyController;
    private BaseMoveAction _moveAction;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _moveAction = GetComponent<BaseMoveAction>();
    }

    public override bool Evaluate()
    {
        if (_playerDetect.TryGetPlayer(out Vector2 playerPosition) && CheckDirection(playerPosition.x))
        {
            //_lastAggressivenessTime = Time.time;

            return true;
        }

        //if (_lastAggressivenessTime + _minAggressivenessTime > Time.time)
        //{
        //    return true;
        //}

        return false;
    }

    public override void Execute()
    {
        _enemyController.OnAttackInputHandler();
    }

    private bool CheckDirection(float playerPositionX)
    {
        var temp = playerPositionX - gameObject.transform.position.x;
        var playerPosDirection = temp switch
        {
            > 0 => FaceDirection.Right,
            < 0 => FaceDirection.Left,
            _ => FaceDirection.Right,
        };

        return playerPosDirection == _moveAction.Direction;
    }
}
