using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    // A Player is a Unit that targets an Enemy and can collect Powerups

    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float rotationInput;
    private InputAction jumpAction;

    //private bool performJump = false;

    // Awake is called once when GameObject is loaded regardless if the script is enabled
    protected override void Awake()
    {
        // Instantiate the generated actions class
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        
        base.Awake();
    }

    private void OnEnable()
    {
        // Subscribe to the move action's performed event

        inputActions.Player.Move.performed += OnMovePerformed;// ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Rotate.performed += OnRotatePerformed;
        inputActions.Player.Rotate.canceled += OnRotatePerformed;
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Fire.performed += OnFirePerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;// ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Player.Rotate.performed -= OnRotatePerformed;
        inputActions.Player.Rotate.canceled -= OnRotatePerformed;
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Fire.performed -= OnFirePerformed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Move was pressed");
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
    }

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
        Debug.Log("Rotating " +  rotationInput);
        //transform.Rotate
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        // Jump Action here
        if (moveInput != Vector2.zero)
        {
            Vector2 moveInputNormalized = moveInput.normalized;
            Vector3 targetPosition = new Vector3(moveInputNormalized.x, 0, moveInputNormalized.y);

            Move(targetPosition);
        }
    }

    protected override void InitializeHealth()
    {
        //throw new System.NotImplementedException();
    }

    protected override void AcquireTarget()
    {
        //throw new System.NotImplementedException();
    }

    protected override void AttackTarget()
    {
        //throw new System.NotImplementedException();
    }

    /*protected override void Move(Vector3 targetDestination)
    {
        //throw new System.NotImplementedException();
        //unitsRigidbody.MovePosition()
    }*/

    protected override void EndUnit()
    {
        base.EndUnit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Powerup collected");
            Destroy(other.gameObject);
        }
    }
}
