using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerInteractable : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableObject;
    private PlayerController player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ActivateIfPlayer(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateIfPlayer(collision.gameObject);
    }

    private void ActivateIfPlayer(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<PlayerController>(out player))
        {
            foreach (var item in _interactableObject)
            {
                item.Activate(player);
            }
        }
    }
}
