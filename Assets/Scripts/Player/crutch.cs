using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crutch : MonoBehaviour
{
    public Transform player;
    private bool facingleft = true;

    public KeyCode chargeKey = KeyCode.H;

    private float cooldownTimer = 0f;
    public float spawnCooldown = 3f;
    private bool attack = false;

    void FixedUpdate()
    {
        if (Input.GetKey(chargeKey))
        {   

            //Debug.Log(attack);

            if (!attack)
            {
                cooldownTimer = spawnCooldown;
                // Start or continue charging
                GetComponent<ColliderDelay>().Use();
                
                attack = true;
            }

        }

        if (attack)
        {   
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                attack = false;

            }
        }
        float playerInput = Input.GetAxis("Horizontal");
        if (playerInput > 0)
        {
            gameObject.transform.position = new Vector3(player.position.x +0.5f, player.position.y+0.5f, player.position.z);
            if (facingleft)
            {
                gameObject.transform.localScale = new Vector3(-1*gameObject.transform.localScale.x,gameObject.transform.localScale.y,1);
                facingleft = false;
            }
            
        }else if(playerInput < 0)
        {
            gameObject.transform.position = new Vector3(player.position.x -0.5f, player.position.y+0.5f, player.position.z);
            if (!facingleft)
            {
                gameObject.transform.localScale = new Vector3(-1*gameObject.transform.localScale.x,gameObject.transform.localScale.y,1);
                facingleft = true;
            }
        }
        
    }
}
