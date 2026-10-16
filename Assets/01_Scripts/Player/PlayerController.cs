using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public bool IsMoving => movement.sqrMagnitude > 0.01f;

    private Rigidbody body;
    private Vector3 movement;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null && (GameManager.Instance == null || !GameManager.Instance.IsEnteringCode))
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1f;
        }

        movement = new Vector3(input.x, 0f, input.y).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = transform.position + movement * speed * Time.fixedDeltaTime;
        nextPosition.x = Mathf.Clamp(nextPosition.x, -7f, 7f);
        nextPosition.z = Mathf.Clamp(nextPosition.z, -5.5f, 7f);

        if (body != null)
        {
            body.MovePosition(nextPosition);
            if (IsMoving)
            {
                body.MoveRotation(Quaternion.LookRotation(movement));
            }
        }
        else
        {
            transform.position = nextPosition;
            if (IsMoving)
            {
                transform.rotation = Quaternion.LookRotation(movement);
            }
        }
    }
}
