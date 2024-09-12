using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ConfigurableCharacterBehaviour
{
    [SerializeField] float speed;

    private Transform body;
    private PlayerInputActions playerControls;
    [SerializeField] InputAction move;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private new void Start()
    {
        base.Start();

        body = GetComponent<Transform>();
    }
    
    override protected void ConfigureValues()
    {
        speed = config.speed;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        Move(move);
    }

    /*
     * For continous movint it is handled in Update method
     * Event based trigger only supports OnPress and OnRelease events, Building the same requires much more code.
     * For handling discrete steps the event based solution seems the better choice. move.performed += Move(InputAction
     */
    private void Move(InputAction action)
    {
        if (action.IsPressed())
        {
            moveDirection = action.ReadValue<Vector2>().normalized;
            Debug.Log("Move!");
            Debug.Log($"Move: [{moveDirection.x}, {moveDirection.y}]");
            body.Translate(new Vector3(moveDirection.x * speed, moveDirection.y * speed) * Time.deltaTime);
        }
    }
}
