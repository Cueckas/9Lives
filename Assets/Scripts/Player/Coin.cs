using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public VoidEventChannel coinEvent;

    private void OnTriggerEnter2D(Collider2D collision){
        try
        {
            String s = collision.gameObject.tag;
        if ( s == "Player")
        {
            coinEvent.Broadcast();
            Destroy(gameObject);   
        }
        }
        catch (NullReferenceException)
        {
            //GameObject.FindGameObjectWithTag("Text").GetComponent<GameManager>().end(1);
        } 
        
    }
}
