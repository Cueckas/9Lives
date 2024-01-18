using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHole : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("in small hole");
            other.gameObject.GetComponent<PlayerManager>().isInSmallHole = true;
            // Player entered the trigger area
            //yourScript.SetVariable(true); // Call a method in YourScript to update the variable
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if(other.gameObject.GetComponent<PlayerManager>().isYoung != true){

                other.gameObject.GetComponent<PlayerManager>().increaseSize_OutOfHole();
                Debug.Log("byeeeeeeeeeeeeeeeeeeeeee");
            }

            // Player exited the trigger area
            //yourScript.SetVariable(false); // Call a method in YourScript to update the variable
        }
    }

}
