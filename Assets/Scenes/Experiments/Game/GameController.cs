using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Character characterPrefab;
    void Start()
    {
        Character spawned = Instantiate(characterPrefab, new Vector3(0, 2), Quaternion.identity);
    }
}
