using UnityEngine;

public enum FaceDirection
{
    Right,
    Left
}

public abstract class BaseMoveAction : MonoBehaviour, ICharacterAction
{
    public FaceDirection Direction => _faceDirection;
    protected FaceDirection _faceDirection;

    protected void FaceFlipDirection(SpriteRenderer renderer, float inputAxisX)
    {
        _faceDirection = inputAxisX switch
        {
            > 0 => FaceDirection.Right,
            < 0 => FaceDirection.Left,
            _ => _faceDirection
        };

        renderer.flipX = _faceDirection != FaceDirection.Right;
    }
    public abstract void Run();
    protected abstract void OnMoveInputHandler(float input);
}
