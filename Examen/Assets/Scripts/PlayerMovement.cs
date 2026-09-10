using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;

    [Header("Salto")]
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Caída")]
    public float fallLimit = -5f;
    public GameOverMenu gameOverMenu;

    private CharacterController controller;

    private Vector2 moveInput;
    private bool jumpInput;

    private float verticalVelocity;
    private bool gameEnded = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (!gameEnded && transform.position.y < fallLimit)
        {
            gameEnded = true;

            Debug.Log("Player Y = " + transform.position.y);

            gameOverMenu.ShowLose();
            return;
        }
        
        // El suelo
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }

            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(
                    jumpHeight * -2f * gravity
                );

                jumpInput = false;
            }
        }

        //Movim rotacion de pj
        transform.Rotate(
            Vector3.up,
            moveInput.x * rotationSpeed * Time.deltaTime
        );


        // Movim desplazamiento de pj
        Vector3 movement =
            transform.forward * moveInput.y;


        controller.Move(
            movement * moveSpeed * Time.deltaTime
        );


        // Gravedad(¿
        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );
    }


    // AndroidInputSystem
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //Debug.Log("Movimiento: " + moveInput);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpInput = true;
        }
    }

    public void JumpButton()
    {
        jumpInput = true;
    }
}

