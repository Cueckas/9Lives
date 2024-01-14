using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bomb;
    public GameObject Player;
    //public Transform parentObject = transform.parent

    private Transform bombTransform;
    private Transform playerTransform;
    public KeyCode chargeKey = KeyCode.X;
    public float maxChargeTime = 3.0f; // Maximum charge time in seconds
    public float damage;

    private bool spawnedBomb = false;
    private bool isCharging = false;
    private float chargeTime = 0.0f;

    public float spawnCooldown = 3f; // Cooldown time in seconds

    private bool canSpawn = true;
    private float cooldownTimer = 0f;
    private Transform spawnBombPosition;

    private float amplitude = 3.0f;
    private float frequency = 0.8f;
    private float speed = 3.0f;

    private float startTime;
    private Vector3 initialBombPosition;
    

    private GameObject referenceBomb;



    void Start()
    {
        // Your initialization or usage code goes here
        
        //bombTransform = GetComponent<Transform>();
        //playerTransform = Player.transform;
        //spawnBombPosition = (playerTransform.position.x, playerTransform.position.x, playerTransform.position.x);
    }

    void Update()
    {
        playerTransform = Player.transform;
        // Example: Spawn the prefab when the space key is pressed
        //Debug.Log(Player.transform.position);

        if (Input.GetKey(chargeKey))
        {   

            //Debug.Log(spawnedBomb);

            if (!spawnedBomb)
            {
                cooldownTimer = spawnCooldown;
                // Start or continue charging
                Debug.Log("eNTERED UPDATE KEY PRSSED");
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Clamp(chargeTime, 0.0f, maxChargeTime);
                Spawn();
                
                spawnedBomb = true;
            }



            //pawn();
        }

        if (spawnedBomb)
        {   
            
            cooldownTimer -= Time.deltaTime;
            float elapsedTime = Time.time - startTime;

            // Calculate the position along the arc using sine and cosine functions
            //float x = amplitude * Mathf.Sin(frequency * elapsedTime * speed);
            float x = speed * elapsedTime;
            //float y = amplitude * Mathf.Cos(frequency * elapsedTime * speed);
            float y = amplitude * Mathf.Sin(frequency * x);

            if (referenceBomb != null){

                Debug.Log(referenceBomb.transform.position);              // Update the object's position
                Transform objectTransform = referenceBomb.transform;

                objectTransform.position = initialBombPosition + new Vector3(x, y, 0f);

                //objectTransform.position = Vector3(x, y, 0f) + 

            }

            if (cooldownTimer <= 0f)
            {
                spawnedBomb = false;

            }
        }

    }

    void Spawn()
    {
        //Vector3 SpawnPoint = playerTransform;

        Debug.Log(playerTransform.position);
        
        Vector3 BombPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        // Instantiate the prefab at the specified spawn point
        GameObject spawnedPrefab = Instantiate(Bomb, BombPosition, playerTransform.rotation);
        referenceBomb = spawnedPrefab;
        initialBombPosition = referenceBomb.transform.position;
        startTime = Time.time;
        //Vector3 position = new Vector3(1.0f, 2.0f, 3.0f);
        // Vector3 slerped = Vector3.Slerp(BombPosition, bombTransform.position * chargeTime, 0.5f);
        //bombTransform.transform.position = slerped;
        Destroy(referenceBomb, 1.1f);

        // Optionally, you can do something with the spawnedPrefab, like setting its properties or adding components.
        // Example: spawnedPrefab.GetComponent<YourScript>().YourMethod();

        // Optionally, you can destroy the spawnedPrefab after a certain time
        //referenceBomb = spawnedPrefab;


        if (Input.GetKeyUp(chargeKey) && isCharging)
        {
            // Execute the charged attack based on charge time
            //throw bomb explode
            // Reset charge variables

            Debug.Log("Charging");
            isCharging = false;
            chargeTime = 0.0f;
            //Destroy(spawnedPrefab.gameObject, 1f);
            
        }

    }




}
