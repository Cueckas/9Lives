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
    public GameObject text;

    void OnEnable()
    {
        text.SetActive(true);
    }
    void FixedUpdate()
    {
        
        if (Input.GetKey(chargeKey))
        {   
            //Debug.Log(attack);

            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,1);
            Invoke("Change",1f);
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

    void Change()
    {
        gameObject.GetComponent<SpriteRenderer>().color += new Color(0,0,0,1);
    }
}
