using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName = "Objeto de prueba";

    public void Interact()
    {
        Debug.Log("Interactuaste con " + objectName);
    }
}
