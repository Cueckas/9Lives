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

    public void Use()
    {
        Invoke("ActivateCollider",0.25f);
        Invoke("HideCollider",0.50f);
    }
    public void ActivateCollider(){

        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void HideCollider(){

        GetComponent<BoxCollider2D>().enabled = false;
    }
}
