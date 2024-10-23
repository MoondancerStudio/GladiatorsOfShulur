using Moondancer.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moondancer.Core
{
    public class RenderableConfiguration : BasicConfiguration
    {
        /**
         * How to retrieve Sprite from path?
         *  Sprite should be stored at MyProject/Assets/Resources/Sprites
         *  <code>Sprite sprite = Resources.Load<Sprite>("Sprites/file_name_without_extension");</code>
         */
        public string iconPath;
    }
}
