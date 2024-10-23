using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatBlockController : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_InputField value;

    public void SetName(string name)
    { 
        this.name.text = name;
    }

    public void SetValue(string value)
    {
        this.value.text = value;
    }
}
