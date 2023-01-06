using UnityEngine;

public class ShotArrowDamage : BaseCollisionDamage
{
    [SerializeField] private int _damage;
    [SerializeField] private float _intervalBetweenDamage = 0f;
    [SerializeField] private float _pushForce;

    private float _previousDamageTime;

    public override void MakeDamage(HealthPoint characterHealthPoint)
    {
        if (_previousDamageTime + _intervalBetweenDamage > Time.time) return;
        _previousDamageTime = Time.time;
        characterHealthPoint.Decrement(_damage);
        
        var normal = characterHealthPoint.gameObject.transform.position - transform.position;
        var forceVelocity = new Vector2(normal.normalized.x * _pushForce, 0);
        _collision.rigidbody.velocity += forceVelocity;

        ArrowDestroy();
    }

    protected override void ToUndamaged() 
    {
        ArrowDestroy();
    }

    private void ArrowDestroy()
    {
        Destroy(gameObject);
    }
}
