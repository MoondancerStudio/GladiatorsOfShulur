using Moondancer.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetController : MonoBehaviour
{
    [SerializeField] private StatisticConfig config;
    [SerializeField] private GameObject statBlock;

    public void Start()
    {
        StatBlockController statBlockController = statBlock.GetComponent<StatBlockController>();

        statBlockController?.SetName(config.name);
        statBlockController?.SetValue(config.value.ToString());
    }
}
