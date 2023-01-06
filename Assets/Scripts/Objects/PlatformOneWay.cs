using System.Collections;
using UnityEngine;

public class PlatformOneWay : MonoBehaviour
{
    private Collider2D _playerCollider;
    private Collider2D _platformCollider;
    private PlayerController _playerController;

    private void Awake()
    {
        _platformCollider = gameObject.GetComponent<Collider2D>();
    }

    public void Disable(bool _)
    {
        StartCoroutine(DisableCollision());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerCollider = collision.gameObject.GetComponent<Collider2D>();
            _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.OnJumpAndDownInput += Disable;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.OnJumpAndDownInput -= Disable;
        }
    }

    private IEnumerator DisableCollision()
    {
        Physics2D.IgnoreCollision(_playerCollider, _platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(_playerCollider, _platformCollider, false);
    }
}
