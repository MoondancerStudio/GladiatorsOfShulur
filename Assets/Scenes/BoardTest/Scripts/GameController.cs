using UnityEngine;

namespace Scenes.BoardTest.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int sizeX = 5, sizeY = 5;
        [SerializeField] private BoardTile tilePrefab;

        [SerializeField] private Transform cam;

        public void Start()
        {
            BuildBoardGrid();

            PositionCamera();
        }

        private void BuildBoardGrid()
        {

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    BoardTile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                    bool isOffset = IsOffset(x, y);
                    spawnedTile.Init(isOffset);
                }
            }
        }

        /**
         * (0, 1); (1, 2)
         */
        private bool IsOffset(int x, int y)
        {
            return (x + y) % 2 != 0;
        }

        private void PositionCamera()
        {
            cam.position = new Vector3(sizeX / 2f - 0.5f, sizeY / 2f -0.5f, -10);
        }
    }
}
