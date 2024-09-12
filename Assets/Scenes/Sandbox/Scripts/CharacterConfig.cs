using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Config")]
public class CharacterConfig : ScriptableObject
{
    public string characterName;
    public int maxHealth;
    public int attackBase;
    public int defenseBase;
    public float speed;

    public Sprite icon;
    public Sprite avatar;
}
