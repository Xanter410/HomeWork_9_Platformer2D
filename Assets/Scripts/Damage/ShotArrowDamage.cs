using System.Collections;
using UnityEngine;

public class ShotArrowDamage : BaseCollisionDamage
{
    [SerializeField] private int _damage;
    [SerializeField] private float _intervalBetweenDamage = 0f;
    [SerializeField] private float _pushForce;

    [SerializeField] private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private float _previousDamageTime;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void MakeDamage(HealthPoint characterHealthPoint)
    {
        if (_previousDamageTime + _intervalBetweenDamage > Time.time) return;
        _previousDamageTime = Time.time;
        characterHealthPoint.Decrement(_damage);
        
        var normal = characterHealthPoint.gameObject.transform.position - transform.position;
        var forceVelocity = new Vector2(normal.normalized.x * _pushForce, 0);
        _collision.rigidbody.velocity += forceVelocity;

        StartCoroutine(ArrowDestroy());
    }

    protected override void ToUndamaged() 
    {
        StartCoroutine(ArrowDestroy());
    }

    IEnumerator ArrowDestroy()
    {
        _spriteRenderer.enabled = false;
        _audioSource.Play();

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
