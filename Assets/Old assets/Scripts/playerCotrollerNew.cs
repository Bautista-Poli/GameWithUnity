using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerNew : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 0.1f;
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;
    private float pitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // --- Movimiento WASD ---
        float h = 0f;
        float v = 0f;

        if (keyboard.aKey.isPressed) h -= 1f;
        if (keyboard.dKey.isPressed) h += 1f;
        if (keyboard.wKey.isPressed) v += 1f;
        if (keyboard.sKey.isPressed) v -= 1f;

        Vector3 move = (transform.right * h + transform.forward * v).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Pasar velocidad al Animator (si existe)
        if (animator != null)
        {
            float speedValue = move.magnitude;
            animator.SetFloat("Speed", speedValue);
        }

        // --- Gravedad + salto ---
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (keyboard.spaceKey.wasPressedThisFrame && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Rotar con el mouse ---
        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 delta = mouse.delta.ReadValue() * mouseSensitivity;

            // VALIDAR que delta no sea NaN o infinito
            if (float.IsNaN(delta.x) || float.IsNaN(delta.y) || 
                float.IsInfinity(delta.x) || float.IsInfinity(delta.y))
            {
                return; // Salir si los valores son inválidos
            }

            // Limitar valores extremos del mouse
            delta.x = Mathf.Clamp(delta.x, -20f, 20f);
            delta.y = Mathf.Clamp(delta.y, -20f, 20f);

            // Rotación horizontal del cuerpo
            transform.Rotate(Vector3.up * delta.x);

            // Rotación vertical SOLO de la cámara
            pitch -= delta.y;
            pitch = Mathf.Clamp(pitch, -80f, 80f);

            // VALIDAR que pitch sea válido antes de crear el Quaternion
            if (!float.IsNaN(pitch) && !float.IsInfinity(pitch))
            {
                cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            }
        }
    }
}


