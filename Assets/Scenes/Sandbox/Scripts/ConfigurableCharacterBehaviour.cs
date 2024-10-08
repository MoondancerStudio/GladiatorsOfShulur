﻿using UnityEngine;

public abstract class ConfigurableCharacterBehaviour : MonoBehaviour
{
    [SerializeField] protected CharacterConfig config;

    public void SetConfig(CharacterConfig config)
    {
        this.config = config; 
    }

    protected void Start()
    {
        ConfigureValues();
    }

    abstract protected void ConfigureValues();
}