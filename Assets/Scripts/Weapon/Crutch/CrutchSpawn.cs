using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrutchSpawn : MonoBehaviour

{


    public GameObject Crutch;
    public GameObject Player;
    //public Transform parentObject = transform.parent

    private Transform crutchTransform;
    private Transform playerTransform;

    public KeyCode chargeKey;

    public float spawnCooldown = 3f;

    private bool spawnedCrutch = false;
    private bool isCharging = false;
    private float chargeTime = 0.0f;

    private bool canSpawn = true;
    private float cooldownTimer = 0f;
    public float maxChargeTime = 3.0f;
    private Transform spawnCrutchPosition;


    private float amplitude = 3.0f;
    private float frequency = 0.8f;
    private float speed = 3.0f;


    private float startTime;
    private Vector3 initialCrutchPosition;
    private bool facingRight = false;
    private bool alive = false;
    //float horizontalInput = Input.GetAxis("Horizontal");


    private GameObject referenceCrutch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = Player.transform;
        // Example: Spawn the prefab when the space key is pressed
        //Debug.Log(Player.transform.position);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            facingRight = Player.transform.GetChild(0).GetComponent<TarodevController.PlayerAnimator>().HandleAnimeFlip();
            
        }

        if (Input.GetKey(chargeKey))
        {

            //Debug.Log(spawnedScythe);

            if (!spawnedCrutch)
            {
                cooldownTimer = spawnCooldown;
                // Start or continue charging
                
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Clamp(chargeTime, 0.0f, maxChargeTime);
                Spawn();

                spawnedCrutch = true;
            }


            //pawn();
        }

        if (spawnedCrutch)
        {

            cooldownTimer -= Time.deltaTime;
            float elapsedTime = Time.time - startTime;

            // Calculate the position along the arc using sine and cosine functions
            //float x = amplitude * Mathf.Sin(frequency * elapsedTime * speed);
            float x = playerTransform.position.x;
            //float y = amplitude * Mathf.Cos(frequency * elapsedTime * speed);
            float y = playerTransform.position.y;

            if (referenceCrutch != null)
            {

                 // Update the object's position
                

                referenceCrutch.transform.position += new Vector3(x,y-1f,0f);
                

                //+ new Vector3(x-0., y - 1.4f, 0f);

                //objectTransform.position = Vector3(x, y, 0f) + 

            }

            if (cooldownTimer <= 0f)
            {
                spawnedCrutch = false;

            }
        }

    }

    void MoveToRight()
    {
        //Debug.Log("entrei no MoveRight");
        
    }

    void MoveToLeft()
    {
        Transform objectTransform = referenceCrutch.transform;

        objectTransform.position = initialCrutchPosition + new Vector3(playerTransform.position.x - 0.5f, playerTransform.position.y- 1f, 0f);
    }

    void Flip()
    {   
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
    }


    void Spawn()
    {
        //Vector3 SpawnPoint = playerTransform;

        
        //Debug.Log(playerTransform.position);

        Vector3 CrutchPosition = new Vector3(playerTransform.position.x , playerTransform.position.y , playerTransform.position.z);
        // Instantiate the prefab at the specified spawn point
        GameObject referenceCrutch = Instantiate(Crutch, CrutchPosition, playerTransform.rotation);
        referenceCrutch.GetComponent<DoDamage>().damage = gameObject.transform.parent.parent.gameObject.GetComponent<CatStats>().attack;
        referenceCrutch.transform.parent = playerTransform;

        startTime = Time.time;

        if (facingRight)
        {   
            //Debug.Log("entrei no facingRight");
           
            //MoveToRight();
            Transform objectTransform = referenceCrutch.transform;

            referenceCrutch.transform.position = new Vector3(playerTransform.position.x + 0.5f, playerTransform.position.y+ 0.5f, 0f);
            referenceCrutch.transform.localScale = new Vector3(-referenceCrutch.transform.localScale.x, referenceCrutch.transform.localScale.y, referenceCrutch.transform.localScale.z);

        }

        else
        {
            

            referenceCrutch.transform.position = new Vector3(playerTransform.position.x - 0.5f, playerTransform.position.y + 0.5f, 0f);

            //Debug.Log("entrei no facingRight=false");
            //MoveToLeft();
        }

        Destroy(referenceCrutch, 1f);

        // Optionally, you can do something with the spawnedPrefab, like setting its properties or adding components.
        // Example: spawnedPrefab.GetComponent<YourScript>().YourMethod();

        // Optionally, you can destroy the spawnedPrefab after a certain time
        //referenceBomb = spawnedPrefab;


        if (Input.GetKeyUp(chargeKey) && isCharging)
        {
            // Execute the charged attack based on charge time
            //throw bomb explode
            // Reset charge variables

            //Debug.Log("Charging");
            isCharging = false;
            chargeTime = 0.0f;
            //Destroy(spawnedPrefab.gameObject, 1f);
            
        }


    }


}
