using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    private Boss boss;
    private bool isExit;

    void Start()
    {
        boss = GetComponent<Boss>();
    }

    private void FixedUpdate()
    {
        if (!isExit && boss?.Lifes == 0)
        {
            isExit = true;
            Invoke(nameof(Win), 5);
        }
    }

    private void Win()
    {
        Debug.Log(true);
        SceneManager.LoadScene(2);
    }
}
