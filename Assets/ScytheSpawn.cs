using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSpawn : MonoBehaviour

{


    public GameObject Scythe;
    public GameObject Player;
    //public Transform parentObject = transform.parent

    private Transform scytheTransform;
    private Transform playerTransform;

    public KeyCode chargeKey = KeyCode.X;

    public float spawnCooldown = 3f;

    private bool spawnedScythe = false;
    private bool isCharging = false;
    private float chargeTime = 0.0f;

    private bool canSpawn = true;
    private float cooldownTimer = 0f;
    public float maxChargeTime = 3.0f;
    private Transform spawnScythePosition;


    private float amplitude = 3.0f;
    private float frequency = 0.8f;
    private float speed = 3.0f;


    private float startTime;
    private Vector3 initialScythePosition;
    


    private GameObject referenceBomb;


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

        if (Input.GetKey(chargeKey))
        {

            //Debug.Log(spawnedBomb);

            if (!spawnedScythe)
            {
                cooldownTimer = spawnCooldown;
                // Start or continue charging
                Debug.Log("eNTERED UPDATE KEY PRSSED");
                isCharging = true;
                chargeTime += Time.deltaTime;
                chargeTime = Mathf.Clamp(chargeTime, 0.0f, maxChargeTime);
                //Spawn();

                spawnedScythe = true;
            }



            //pawn();
        }

        if (spawnedScythe)
        {

            cooldownTimer -= Time.deltaTime;
            float elapsedTime = Time.time - startTime;

            // Calculate the position along the arc using sine and cosine functions
            //float x = amplitude * Mathf.Sin(frequency * elapsedTime * speed);
            float x = speed * elapsedTime;
            //float y = amplitude * Mathf.Cos(frequency * elapsedTime * speed);
            float y = amplitude * Mathf.Sin(frequency * x);

            if (referenceBomb != null)
            {

                Debug.Log(referenceBomb.transform.position);              // Update the object's position
                Transform objectTransform = referenceBomb.transform;

                objectTransform.position = initialScythePosition + new Vector3(x, y, 0f);

                //objectTransform.position = Vector3(x, y, 0f) + 

            }

            if (cooldownTimer <= 0f)
            {
                spawnedScythe = false;

            }
        }

    }
}
