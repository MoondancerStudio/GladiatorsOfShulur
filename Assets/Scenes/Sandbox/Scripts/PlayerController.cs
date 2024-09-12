using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ConfigurableCharacterBehaviour
{
    [SerializeField] Transform body;

    [SerializeField] float speed;
    [SerializeField] InputAction playerControls;

    Vector2 moveDirection = Vector2.zero;

    public new void Start()
    {
        base.Start();

        body = GetComponent<Transform>();
    }
    override protected void ConfigureValues()
    {
        speed = config.speed;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    public void Update()
    {      
        moveDirection = playerControls.ReadValue<Vector2>().normalized;

        Debug.Log($"Move: [{moveDirection.x}, {moveDirection.y}]");
    }

    private void FixedUpdate()
    {
        body.Translate(new Vector3(moveDirection.x * speed, moveDirection.y * speed) * Time.deltaTime);
    }
}
