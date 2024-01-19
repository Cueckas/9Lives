using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgame : MonoBehaviour
{
    public GameObject panel;
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Rank()
    {
        panel.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(4);
    }
}
