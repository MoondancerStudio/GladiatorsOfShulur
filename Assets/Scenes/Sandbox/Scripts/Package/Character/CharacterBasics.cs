using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moondancer.Core;

namespace Moondancer.Character
{
    public class CharacterBasics : ConfigurablBehaviour<BasicConfiguration>
    {
        private string description;

        public override void SetConfig(BasicConfiguration config)
        {
            this.name = config.name;
            this.description = config.description;
        }
    }
}
