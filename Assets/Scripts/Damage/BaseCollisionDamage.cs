using UnityEngine;

public abstract class BaseCollisionDamage : MonoBehaviour
{
    [SerializeField] protected bool _doEnemyTakeDamage;
    [SerializeField] protected bool _doPlayerTakeDamage;

    protected Collision2D _collision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collision = collision;
        EvaluateCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _collision = collision;
        EvaluateCollision(collision.gameObject);
    }

    private void EvaluateCollision(GameObject go)
    {
        if (go.CompareTag("Player") == _doPlayerTakeDamage || go.CompareTag("Enemy") == _doEnemyTakeDamage)
        {
            if (go.TryGetComponent<HealthPoint>(out var characterHealthPoint))
            {
                if (characterHealthPoint.IsAlive)
                {
                    MakeDamage(characterHealthPoint);
                }
            }
            else
            {
                ToUndamaged();
            }
        }
    }

    public abstract void MakeDamage(HealthPoint characterHealthPoint);

    protected virtual void ToUndamaged() { }
}
