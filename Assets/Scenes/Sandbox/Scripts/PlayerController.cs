using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float speed;

    //private Transform body;
    private PlayerInputActions playerControls;
    private Camera camera;
    [SerializeField] private InputAction move;
    [SerializeField] private InputAction fire;

    public GameEvent playerMoveEvent;
    public GameEvent playerAttackEvent;

    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        camera = Camera.main;
    }

    public void Start()
    {
        //body = GetComponent<Transform>();
        if (GetComponent<CharacterMove>() != null)
        {
            speed = GetComponent<CharacterMove>().GetSpeed();
        }
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Attack;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
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
            // TODO: Refactor this in a way where the actual move is the responsibility of the CharacterMove component.
            playerMoveEvent.TriggerEvent();
            //playerMoveEvent.TriggerEvent(moveDirection);
            Debug.Log($"{name}: Should have moved! [{moveDirection.x}, {moveDirection.y}]");
            //body.Translate(new Vector3(moveDirection.x * speed, moveDirection.y * speed) * Time.deltaTime);
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        var mousePosition = Mouse.current.position;
        var attackVector = camera.ScreenToWorldPoint(new Vector3(mousePosition.x.value, mousePosition.y.value));

        // TODO: Refactor this in a way where the actual move is the responsibility of the CharacterCombat component.
        // find target by raycasting
        var targetName = "John Doe";
        //playerAttackEvent.TriggerEvent(target);
        playerAttackEvent.TriggerEvent();
        Debug.Log($"{name}: Attack {targetName}! [{attackVector.x}, {attackVector.y}]");


    }
}
