using Moondancer.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiCharacterSheet : MonoBehaviour
{
    private TMP_InputField input;

    void Awake()
    {
        input = GetComponent<TMP_InputField>();

        input.text = "John Doe";
    }
}
