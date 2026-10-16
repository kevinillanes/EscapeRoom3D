using UnityEngine;

public class OldDoorController : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;

    public void OpenDoor()
    {
        transform.Rotate(0f, openAngle, 0f);
    }
}
