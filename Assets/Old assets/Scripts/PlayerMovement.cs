using UnityEngine;
using UnityEngine.InputSystem; // ← Importante

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private CharacterController controller;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- Ground check ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // --- Movimiento WASD con New Input System ---
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) vertical += 1;
            if (Keyboard.current.sKey.isPressed) vertical -= 1;
            if (Keyboard.current.dKey.isPressed) horizontal += 1;
            if (Keyboard.current.aKey.isPressed) horizontal -= 1;
        }

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        // --- Salto ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravedad ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

