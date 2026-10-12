using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool unlocked = false;
    [SerializeField] private Animator animator;

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("¡Cofre desbloqueado por el puzzle!");
    }

    public void Interact()
    {
        if (!unlocked)
        {
            Debug.Log("El cofre está cerrado. Necesitas resolver el puzzle.");
            return;
        }

        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        Debug.Log("¡Cofre abierto! Has obtenido la llave.");
    }
}