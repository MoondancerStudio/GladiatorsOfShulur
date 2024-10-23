using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moondancer.Core
{
    public class ConfigurationHolder<C> : MonoBehaviour where C : ScriptableObject
    {
        [SerializeField] private C config;

        public void Awake()
        {
            if (config == null)
            {
                throw new System.ArgumentNullException("config");
            }

            List<ConfigurablBehaviour<C>> behaviours = new(this.GetComponentsInParent<ConfigurablBehaviour<C>>());
            behaviours.ForEach(behaviour => behaviour.SetConfig(config));
        }
    }
}
