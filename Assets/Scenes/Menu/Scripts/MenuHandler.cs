using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void startGame()
    {
        Debug.Log("load");
        SceneManager.LoadScene("game");
    }

    void Update()
    {
        
    }
}
