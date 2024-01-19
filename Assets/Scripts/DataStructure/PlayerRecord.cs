using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord 
{
    public String name;

    public float score;

    public PlayerRecord(float s)
    {
        score = s;
    }

     public static int SortForm(PlayerRecord p1, PlayerRecord p2)
    {
        return p1.score.CompareTo(p2.score);
    }
}
