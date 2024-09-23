using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITem
{
    void OnEquip(GameObject character);
    void OnRemove(GameObject character);
}
