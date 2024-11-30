using Scenes.BoardTest.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private int x, y;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Dropzone dropzonePrefab;

    public void Start()
    {
        this.transform.position = new Vector3(x, y, 0);
    }

    public void OnMouseUp()
    {
        spriteRenderer.color = Color.red;
        ShowDropZones();
    }

    private void ShowDropZones()
    {
        for (int deltaX = -1; deltaX <= 1; deltaX++)
        {
            for (int deltaY = -1; deltaY <= 1; deltaY++)
            {
                if ((deltaX != 0 || deltaY != 0) && x + deltaX >= 0 && y + deltaY >= 0)
                {
                    Dropzone spawnedTile = Instantiate(dropzonePrefab, new Vector3(x + deltaX, y + deltaY, -0.1f), Quaternion.identity);
                }
            }
        }
        
    }
}
