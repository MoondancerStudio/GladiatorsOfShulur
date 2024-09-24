using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    void OnEquip(GameObject character);
    void OnRemove(GameObject character);
}
