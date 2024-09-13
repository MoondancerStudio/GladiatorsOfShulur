using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float speed;

    private PlayerInputActions playerControls;
    private new Camera camera;
    [SerializeField] private InputAction move;
    [SerializeField] private InputAction fire;

    public GameEvent2 playerMoveEvent;
    public GameEvent2 playerAttackEvent;

    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        camera = Camera.main;
    }

    public void Start()
    {
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

    private void Move(InputAction action)
    {
        if (action.IsPressed())
        {
            moveDirection = action.ReadValue<Vector2>().normalized;

            EventParameter eventParameter = ScriptableObject.CreateInstance<EventParameter>();
            eventParameter.Add("move", moveDirection);

            playerMoveEvent.TriggerEvent(eventParameter);
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        var mousePosition = Mouse.current.position;
        var attackVector = camera.ScreenToWorldPoint(new Vector3(mousePosition.x.value, mousePosition.y.value));

        EventParameter eventParameter = ScriptableObject.CreateInstance<EventParameter>();
        eventParameter.Add("attackVector", attackVector);

        playerAttackEvent.TriggerEvent(eventParameter);
    }
}
