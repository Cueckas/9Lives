using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float perRadian = 0.01f;
    float radian = 0;
    float radius =0.0025f;
    float dy;
    public int type;

    bool change = true;

    void FixedUpdate()
    {
        radian +=perRadian;
        dy = Mathf.Cos(radian)*radius;
        transform.position += new Vector3(0,dy,0); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent.GetChild(type).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
