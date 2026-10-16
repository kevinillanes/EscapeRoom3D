using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)) input.y += 1;
            if ((Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)) input.y -= 1;
            if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)) input.x -= 1;
            if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)) input.x += 1;
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y);

        if (movement.magnitude > 1f)
            movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime);
    }
}