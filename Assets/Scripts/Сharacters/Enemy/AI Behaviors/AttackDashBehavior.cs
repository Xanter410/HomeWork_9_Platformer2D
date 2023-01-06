using UnityEngine;

public class AttackDashBehavior : Behavior
{
    [SerializeField] private PlayerDetectZone _playerDetect;
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
            return true;
        }

        return false;
    }

    public override void Execute()
    {
        _enemyController.OnDashInputHandler();
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
