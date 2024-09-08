using UnityEngine;

public class ConfigurableCharacterBehaviour : MonoBehaviour
{
    [SerializeField] protected CharacterConfig config;

    public void SetConfig(CharacterConfig config)
    {
        this.config = config; 
    }
}