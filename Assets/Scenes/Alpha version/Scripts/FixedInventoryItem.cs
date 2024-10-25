using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class FixedInventoryItem : ScriptableObject
{
    public bool hasItem;
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;
}
