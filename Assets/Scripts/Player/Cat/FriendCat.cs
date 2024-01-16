using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FriendCat : MonoBehaviour
{

    public int type;
    public Sprite newSprite;
    public GenerateNextGeneration gg;

    private bool touched = false;

    private void OnEnable()
    {
        type = UnityEngine.Random.Range(0,7);
        if(type == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color((204f/255f),1f,(153f/255f),1f);
        }else if(type == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }else if(type == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color((102f/255f),1f,1f,1f);
        }else if(type == 4)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }else if(type == 5)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,(153f/255f),1f,1f);
        }
    }
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
