using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moondancer.Core
{
    public abstract class ConfigurablBehaviour<C> : MonoBehaviour where C : ScriptableObject
    {
        /**
         * Digest configuration, <code>like this.name = config.name;</code>
         */
        abstract public void SetConfig(C config);
    }
}
