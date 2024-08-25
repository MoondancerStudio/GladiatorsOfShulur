using System;
using UnityEngine;

namespace Scenes.BoardTest.Scripts
{
    public class BoardTile : Highlightable
    {
        [SerializeField] private Color primaryColor, secondaryColor;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Init(bool isOffset = false)
        {
            spriteRenderer.color = isOffset ? secondaryColor : primaryColor;
        }
    }
}
