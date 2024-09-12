using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBasics : ConfigurableCharacterBehaviour
{
    protected override void ConfigureValues()
    {
        this.name = config.characterName;
        GetComponent<SpriteRenderer>().sprite = config.icon;
    }
}
