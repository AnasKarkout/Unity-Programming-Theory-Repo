using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    // A Player is a Unit that targets an Enemy and can collect Powerups
    [SerializeField] private float jumpHeight = 5.0f;

    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float rotationInput;
    private bool performJump = false;
    private bool isOnGround = true;

    // Awake is called once when GameObject is loaded regardless if the script is enabled
    protected override void Awake()
    {
        base.Awake();

        // Instantiate the generated actions class
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        // Subscribe to the action's performed event
        inputActions.Player.Move.performed += OnMovePerformed;// ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += OnMovePerformed;// ctx => moveInput = Vector2.zero;
        inputActions.Player.Rotate.performed += OnRotatePerformed;
        inputActions.Player.Rotate.canceled += OnRotatePerformed;
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Fire.performed += OnFirePerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action's performed event
        inputActions.Player.Move.performed -= OnMovePerformed;// ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= OnMovePerformed;// ctx => moveInput = Vector2.zero;
        inputActions.Player.Rotate.performed -= OnRotatePerformed;
        inputActions.Player.Rotate.canceled -= OnRotatePerformed;
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Fire.performed -= OnFirePerformed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        DoJump();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
        if (rotationInput != 0)
        {
            overrideRotation = true;
        }
        else
        {
            overrideRotation = false;
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isOnGround)
        {
            performJump = true;
        }
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (target != null)
        {
            AttackTarget();
        }
        else
        {
            AcquireTarget();
            if (target != null)
            {
                AttackTarget();
            }
        }
    }

    protected override void DoMove()
    {
        if (moveInput != Vector2.zero)
        {
            // in this scene Vector2 is defined as (x = right, y = forward)
            Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
            Vector3 targetPosition = transform.position + new Vector3(moveDirection.x, 0, moveDirection.z);
            Move(targetPosition);
        }
    }

    private void DoJump()
    {
        if (performJump)
        {
            Vector3 currentVelocity = unitsRigidbody.linearVelocity;
            currentVelocity.y = jumpHeight;
            unitsRigidbody.linearVelocity = currentVelocity; // This ensures consistent jump height ignoring physics unlike impulse
            isOnGround = false;
            performJump = false;
        }
    }

    protected override void DoRotation()
    {
        if (rotationInput != 0)
        {
            targetRotation = unitsRigidbody.rotation * Quaternion.Euler(0, rotationInput * rotationSpeed, 0);
            isLookingAtTarget = false;
            Rotate();
        }
        else
        {
            if (target != null)
            {
                // TODO figure out why this makes the player rotation choppy
                //base.DoRotation();
            }
        }
    }

    protected override void AcquireTarget()
    {
        if (target == null)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            if (targets.Length > 0)
            {
                target = targets
                    .OrderBy(go => (go.transform.position - transform.position).sqrMagnitude)
                    .First()
                    .GetComponent<Unit>();
            }
        }
    }

    protected override void EndUnit()
    {
        inputActions.Disable();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
