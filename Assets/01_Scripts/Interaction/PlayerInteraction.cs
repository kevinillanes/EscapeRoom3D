using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2.4f;

    private void Update()
    {
        if (Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame)
        {
            return;
        }

        if (GameManager.Instance != null && GameManager.Instance.IsEnteringCode)
        {
            return;
        }

        IInteractable closest = FindClosestInteractable();
        if (closest != null)
        {
            closest.Interact();
        }
        else if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage("No hay nada para interactuar cerca.", 2f);
        }
    }

    private IInteractable FindClosestInteractable()
    {
        MonoBehaviour[] behaviours = FindObjectsByType<MonoBehaviour>();
        IInteractable closest = null;
        float closestDistance = interactionDistance;

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (!behaviour.isActiveAndEnabled || !(behaviour is KeyItem key))
            {
                continue;
            }

            if (Vector3.Distance(transform.position, behaviour.transform.position) <= interactionDistance)
            {
                return key;
            }
        }

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (!behaviour.isActiveAndEnabled || behaviour.gameObject == gameObject || !(behaviour is IInteractable interactable))
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, behaviour.transform.position);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        return closest;
    }
}
