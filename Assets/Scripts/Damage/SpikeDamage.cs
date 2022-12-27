using UnityEngine;

public class SpikeDamage : BaseCollisionDamage
{
    [SerializeField] private int _damage;
    [SerializeField] private float _intervalBetweenDamage = 0.5f;
    [SerializeField] private float _pushForce;

    private float _previousDamageTime;

    public override void MakeDamage(HealthPoint characterHealthPoint)
    {
        if (_previousDamageTime + _intervalBetweenDamage > Time.time) return;
        _previousDamageTime = Time.time;
        characterHealthPoint.Decrement(_damage);

        var normal = _collision.GetContact(0).normal;
        var forceVelocity = new Vector2(-normal.x * _pushForce, -normal.y * _pushForce);
        _collision.rigidbody.velocity += forceVelocity;
    }
}
