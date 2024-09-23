using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlatformerMove : ConfigurableCharacterBehaviour
{
    private Transform body;
    [SerializeField] private float speed;

    override protected void ConfigureValues()
    {
        body = GetComponent<Transform>();
        speed = config.speed;
    }

    public float GetSpeed() { return speed; }

    public void Move(Vector2 moveDirection)
    {
        if (moveDirection == null)
        {
            moveDirection = Vector2.zero;
        }

        Debug.Log($"{name}: Move! [{moveDirection.x}, {moveDirection.y * 0}]");
        body.Translate(new Vector3(moveDirection.x * speed, moveDirection.y * speed * 0) * Time.deltaTime);
    }

    public void OnMoveControl(EventParameter parameter) 
    {
        if (parameter != null && parameter.Get("move") != null && parameter.Get("move") is Vector2 moveDirection)
        {
            Move(moveDirection);
        }
    }
}
