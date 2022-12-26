using UnityEngine;

[DefaultExecutionOrder(100500)]
public class ContactCheck : MonoBehaviour
{
    private int _groundContactCount;
    public bool IsGrounded => _groundContactCount > 0;

    private void FixedUpdate()
    {
        Clean();
    }

    private void Clean()
    {
        _groundContactCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;
            var dotProduct = Vector3.Dot(normal, Vector2.up);
            _groundContactCount = dotProduct > 0.75 ? _groundContactCount + 1 : _groundContactCount;
        }
    }
}
