using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public void OnMoveControl(EventParameter parameter)
    {
        if (parameter != null && parameter.Get("move") != null && parameter.Get("move") is Vector2 moveDirection)
        {
            GetComponent<CharacterMove>()?.Move(moveDirection);
        }
    }
}
