using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBasics : MonoBehaviour
{
    [SerializeField] private CharacterConfig config;

    public void Awake()
    {
        if (config == null) { 
            throw new System.ArgumentNullException("config");
        }

        this.name = config.characterName;

        List<ConfigurableCharacterBehaviour> behaviours = new(this.GetComponentsInParent<ConfigurableCharacterBehaviour>());
        behaviours.ForEach(behaviour => behaviour.SetConfig(config));
    }
}
