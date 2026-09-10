using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{

    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Animator m_animator;
    private Rigidbody m_Rigidbody;

    public float walkSpeed = 5;
    public float rotateSpeed = 5;
    public float jumpSpeed = 5;


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        m_animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        if (m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

    }

    public void Jump()
    {
        m_Rigidbody.AddForceAtPosition(new Vector3(0,5f,0), Vector3.up, ForceMode.Impulse);
        m_animator.SetTrigger("Jump");
    }


    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Walking()
    {
        m_animator.SetFloat("Speed", m_moveAmt.y);
        m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * m_moveAmt.y * walkSpeed * Time.deltaTime);
    }

    private void Rotating()
    {
        if (m_moveAmt.y != 0)
        {
            float rotationAmaount = m_lookAmt.x * rotateSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmaount, 0);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation *  deltaRotation);
        }
    }
}
