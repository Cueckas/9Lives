using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public List<float> list;

    public float temp;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            list = new List<float>();
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void Add(float score)
    {
        list.Add(score);
        temp = score;
        list.Sort();
    }

    
}
