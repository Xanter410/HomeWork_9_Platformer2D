using UnityEngine;

public class CharacterHitDamage : BaseTriggerDamage
{
    [SerializeField] private int _damage;
    [SerializeField] private float _intervalBetweenDamage = 0f;
    [SerializeField] private float _pushForce;
    [SerializeField] private Transform _character;

    private float _previousDamageTime;

    protected override void MakeDamage(HealthPoint characterHealthPoint)
    {
        if (_previousDamageTime + _intervalBetweenDamage > Time.time) return;
        _previousDamageTime = Time.time;
        characterHealthPoint.Decrement(_damage);

        if (_collider.TryGetComponent(out Rigidbody2D rigidbody))
        {
            var normal = characterHealthPoint.gameObject.transform.position - _character.position;
            var forceVelocity = new Vector2(normal.normalized.x * _pushForce, 0);
            rigidbody.velocity += forceVelocity;
        }
        HitOff();
    }

    protected override void ToUndamaged()
    {
        HitOff();
    }
    private void FixedUpdate()
    {
        HitOff();
    }

    private void HitOff()
    {
        gameObject.SetActive(false);
    }
}