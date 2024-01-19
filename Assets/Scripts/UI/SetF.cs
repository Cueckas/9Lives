using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetF : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("SetFalse",3f);
    }
    
    void SetFalse()
    {
        gameObject.SetActive(false);
    }
}
