using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] private float floatHeight = 0.12f;
    [SerializeField] private float floatSpeed = 2f;

    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        Vector3 position = startPosition;
        position.y += Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = position;
    }

    public void Interact()
    {
        if (GameManager.Instance == null || GameManager.Instance.HasKey)
        {
            return;
        }

        GameManager.Instance.ObtainKey();
        gameObject.SetActive(false);
        Debug.Log("Llave obtenida.");
    }
}
