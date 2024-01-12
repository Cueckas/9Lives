using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCat : MonoBehaviour
{

    public int type;

    public GenerateNextGeneration gg;

    private bool touched = false;
    private void OnTriggerEnter2D(Collider2D collision){
        if (touched)
        {
            return;
        }
        gg.SetMother(type);
        touched = true;
    }
}
