using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDEstroy : MonoBehaviour
{
    public float radius;
    void OnDestroy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position,radius);
        foreach (Collider2D item in colliders)
        {
            String s = item.gameObject.tag;
            if (s == "Block")
            {
                Destroy(item.gameObject);
            }
        }
    }
}
