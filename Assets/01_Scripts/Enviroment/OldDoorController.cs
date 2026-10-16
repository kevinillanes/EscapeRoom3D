using UnityEngine;

public class OldDoorController : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;
        transform.Rotate(0f, openAngle, 0f);
        isOpen = true;
    }
}
