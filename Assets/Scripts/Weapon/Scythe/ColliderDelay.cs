using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateCollider",0.25f);
    }


    public void ActivateCollider(){

        GetComponent<BoxCollider2D>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
