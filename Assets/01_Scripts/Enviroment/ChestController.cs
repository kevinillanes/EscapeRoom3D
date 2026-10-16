using System.Collections;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool unlocked;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puzzleCanvas;
    [SerializeField] private Transform lidPivot;
    [SerializeField] private GameObject keyObject;
    [SerializeField] private float animationDuration = 1.2f;

    private PuzzleManager puzzleManager;
    private bool isOpen;

    public void Configure(Transform lid, GameObject key, PuzzleManager puzzle)
    {
        lidPivot = lid;
        keyObject = key;
        puzzleManager = puzzle;

        if (keyObject != null)
        {
            keyObject.SetActive(false);
        }
    }

    public void Unlock()
    {
        if (unlocked)
        {
            return;
        }

        unlocked = true;
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false);
        }

        GameManager.Instance?.NotifyChestUnlocked();
        Debug.Log("Cofre desbloqueado por el puzzle.");
    }

    public void Interact()
    {
        if (!unlocked)
        {
            if (puzzleManager == null)
            {
                puzzleManager = FindAnyObjectByType<PuzzleManager>();
            }

            if (puzzleManager == null)
            {
                const string warning = "No se encontro un PuzzleManager. No se puede iniciar el puzzle.";
                Debug.LogWarning(warning);
                GameManager.Instance?.ShowMessage(warning);
                return;
            }

            if (puzzleCanvas != null)
            {
                puzzleCanvas.SetActive(true);
            }

            GameManager.Instance?.BeginPuzzle(puzzleManager);
            return;
        }

        if (isOpen)
        {
            GameManager.Instance?.ShowMessage("El cofre ya esta abierto. Recoge la llave.");
            return;
        }

        isOpen = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }

        StartCoroutine(OpenChest());
    }

    private IEnumerator OpenChest()
    {
        if (lidPivot != null)
        {
            Quaternion start = lidPivot.localRotation;
            Quaternion end = start * Quaternion.Euler(-105f, 0f, 0f);
            float elapsed = 0f;

            while (elapsed < animationDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.SmoothStep(0f, 1f, elapsed / animationDuration);
                lidPivot.localRotation = Quaternion.Slerp(start, end, t);
                yield return null;
            }

            lidPivot.localRotation = end;
        }

        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }

        GameManager.Instance?.NotifyChestOpened();
        Debug.Log("Cofre abierto. La llave esta disponible.");

        // El cofre ya termino su funcion. Al desactivar esta interaccion,
        // la siguiente pulsacion de E cerca del cofre recoge la llave.
        enabled = false;
    }
}
