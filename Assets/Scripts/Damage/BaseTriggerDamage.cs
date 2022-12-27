using UnityEngine;

public abstract class BaseTriggerDamage : MonoBehaviour
{
    [SerializeField] protected bool _doEnemyTakeDamage;
    [SerializeField] protected bool _doPlayerTakeDamage;

    protected Collider2D _collider;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _collider = collider;
        EvaluateCollision(collider.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        _collider = collider;
        EvaluateCollision(collider.gameObject);
    }

    private void EvaluateCollision(GameObject go)
    {
        if (go.CompareTag("Player") == _doPlayerTakeDamage || go.CompareTag("Enemy") == _doEnemyTakeDamage)
        {
            if (go.TryGetComponent<HealthPoint>(out var characterHealthPoint))
            {
                MakeDamage(characterHealthPoint);
            }
            else
            {
                ToUndamaged();
            }
        }
    }

    protected abstract void MakeDamage(HealthPoint characterHealthPoint);

    protected virtual void ToUndamaged() { }
}
