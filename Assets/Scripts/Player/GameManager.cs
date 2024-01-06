using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lifeNumber;

    [SerializeField] Text life;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;


    [SerializeField] GameObject deathmenu;

    private bool isPaused = false;  
    void OnEnable()
    {
        dieChannel.AddListener(Die);
        positionChannel.AddListener(NewLife);
        life.text = lifeNumber.ToString("D2");
    }

    void Die()
    {
        lifeNumber -= 1;
        life.text = lifeNumber.ToString("D2");
        Debug.Log("-1");

        toggleDeathMenu();


        if (lifeNumber <= 0)
        {
            GameOver();
        }
    }

    public void toggleDeathMenu()
    {
        isPaused = !isPaused;
        SetMenuPanelActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void SetMenuPanelActive(bool active)
    {
        // Enable or disable the menu panel
        deathmenu.SetActive(active);
    }

 
    void NewLife(Vector3 position)
    {
        if (lifeNumber > 0)
        {
            GameObject g = (GameObject) Resources.Load("Prefabs/Test_prefab");
            Instantiate(g, position, Quaternion.identity);
        }
        
    }

    void GameOver()
    {
        Debug.Log("game over");
    }
}
