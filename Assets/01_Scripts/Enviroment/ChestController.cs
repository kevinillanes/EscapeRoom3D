using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool unlocked = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puzzleCanvas; // <--- NUEVO

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("¡Cofre desbloqueado por el puzzle!");

        // Si el cofre se desbloquea, ocultamos el canvas
        // if (puzzleCanvas != null) puzzleCanvas.SetActive(false);
    }

    public void Interact()
    {
        if (!unlocked)
        {
            Debug.Log("El cofre está cerrado. Necesitas resolver el puzzle.");
            // ¡AQUÍ ESTÁ LA MAGIA! Mostramos el canvas
            if (puzzleCanvas != null) puzzleCanvas.SetActive(true);
            return;
        }

        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        Debug.Log("¡Cofre abierto! Has obtenido la llave.");
    }
}