using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybleBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision){
        try
        {
            String s = collision.gameObject.tag;
        if ( s == "Bomb")
        {
            Destroy(gameObject);   
        }
            Debug.Log(213);
        }
        catch (NullReferenceException)
        {
            //GameObject.FindGameObjectWithTag("Text").GetComponent<GameManager>().end(1);
        } 
        
    }
}
