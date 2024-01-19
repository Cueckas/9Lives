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

    public SpriteRenderer t;
    public Sprite[] typeSprite;

    private bool touched = false;

    private void OnEnable()
    {
        type = UnityEngine.Random.Range(0,4);
        Debug.Log(type);
        t.sprite = typeSprite[type];
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
