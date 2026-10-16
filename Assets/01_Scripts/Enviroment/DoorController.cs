using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = -105f;
    [SerializeField] private float animationDuration = 1.4f;

    private bool isOpen;

    public void Configure(Transform pivot)
    {
        doorPivot = pivot;
    }

    public void Interact()
    {
        if (isOpen)
        {
            return;
        }

        if (GameManager.Instance == null || GameManager.Instance.HasKey)
        {
            GameManager.Instance?.ShowMessage("La puerta esta cerrada. Necesitas la llave del cofre.", 4f);
            return;
        }

        isOpen = true;
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        Transform animatedTransform = doorPivot != null ? doorPivot : transform;
        Quaternion start = animatedTransform.localRotation;
        Quaternion end = start * Quaternion.Euler(0f, openAngle, 0f);
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animationDuration);
            animatedTransform.localRotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }

        animatedTransform.localRotation = end;
        yield return new WaitForSeconds(0.35f);
        GameManager.Instance?.FinishGame();
        Debug.Log("Puerta abierta. Escape completado.");
    }
}
