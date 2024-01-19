using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public List<PlayerRecord> list;

    public PlayerRecord temp;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            list = new List<PlayerRecord>();
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void Add(float score)
    {
        temp = new PlayerRecord(score);
        list.Add(temp);
        
        list.Sort(PlayerRecord.SortForm);
    }

    public void AddName(String name)
    {
        temp.name = name;
    }
}
