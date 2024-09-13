using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : ConfigurableCharacterBehaviour
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

        Debug.Log($"{name}: Move! [{moveDirection.x}, {moveDirection.y}]");
        body.Translate(new Vector3(moveDirection.x * speed, moveDirection.y * speed) * Time.deltaTime);
    }

    public void DoSomething() { }
}
