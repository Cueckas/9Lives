using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCat : MonoBehaviour
{

    public int type;
    public Sprite newSprite;
    public GenerateNextGeneration gg;

    private bool touched = false;
    private void OnTriggerEnter2D(Collider2D collision){
        if (touched)
        {
            return;
        }
        try
        {
            String s = collision.gameObject.tag;
            if ( s == "Player")
            {
                gg.SetMother(type, gameObject.transform.position);
                touched = true;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        catch (NullReferenceException)
        {
            //GameObject.FindGameObjectWithTag("Text").GetComponent<GameManager>().end(1);
        }    
    }
}
