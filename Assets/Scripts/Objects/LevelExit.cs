using UnityEngine;

public class LevelExit : Interactable
{
    [SerializeField] private GameEvent _onLevelCompleted;
    bool _isActive = false;
    public override void Activate(PlayerController player)
    {
        if (!_isActive)
        {
            _isActive = true;

            _onLevelCompleted.TriggerEvent();
        }
    }
}
