using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float perRadian = 0.01f;
    public float radian = 0;
    public float radius =0.0025f;
    float dy;
    public int type;

    public GameObject text;

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
            if (text != null)
            {
                text.SetActive(true);
            }
            collision.gameObject.transform.parent.GetChild(type).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
