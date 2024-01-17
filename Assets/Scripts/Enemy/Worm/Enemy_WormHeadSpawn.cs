using UnityEngine;

public class Enemy_WormHeadSpawn : MonoBehaviour
{
    public GameObject WormHead;
    //public Transform parentObject = transform.parent

    public float damage;

    //bool
    private bool spawnedWormHead = false;

    public float spawnCooldown = 3.5f; // Cooldown time in seconds
    public float hideCooldown = 2f;

    //spawn timers
    private float cooldownTimer = 0f;




    //stats
    private float amplitude = 3.0f;
    private float frequency = 0.8f;
    private float speed = 3.0f;

    //position
    private float x;
    private float y;
    private float currentHp = 0;

    private bool playerCollided = false;


    private float startTime;
    private bool referenceAlive = false;
    
    private GameObject HeadObject;
    private GameObject EnemyWormBody;

    private EnemyPatrolGround patrolScript;
    
    private EnemyStats childEnemyStatsScript;

    void Start()
    {
        patrolScript = GetComponent<EnemyPatrolGround>();
        childEnemyStatsScript = GetComponentInChildren<EnemyStats>();
        HeadObject = gameObject.transform.GetChild(1).gameObject;
        EnemyWormBody = gameObject.transform.GetChild(0).gameObject;

        Physics2D.IgnoreLayerCollision(9, 6, true);

        Debug.Log(HeadObject.name);

    }

    void Update()
    {
        
        cooldownTimer -= Time.deltaTime;

        if (gameObject.transform.childCount < 2)
        {
            Debug.Log("WHAT");
            Die();
        }
        else
        {
            if (!spawnedWormHead && cooldownTimer <= 0)
            {
                cooldownTimer = hideCooldown;
                // Start or continue charging
                Debug.Log("to spawn");
                Activate();
                spawnedWormHead = true;
            }

            if (spawnedWormHead && cooldownTimer <= 0f)
            {
                DeActivate();
                cooldownTimer = spawnCooldown;
                spawnedWormHead = false;

            }

        }
    }
   
    void Activate()
    {
      if (WormHead != null)
         {

          //Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y +1f, transform.position.z);
          //childEnemyStatsScript = HeadObject.GetComponent<EnemyStats>();
          
          //reference = Instantiate(WormHead, spawnPosition, transform.rotation);

          //reference.transform.SetParent(transform);

          HeadObject.SetActive(true);
          EnemyWormBody.SetActive(false);

          GetComponent<EnemyPatrolGround>().stop = true;


            startTime = Time.time;
          //Destroy(reference, 0.5f);

          }

    }

    void DeActivate()
    {
        // Add any death-related logic here (e.g., play death animation, destroy GameObject, etc.)
        referenceAlive = false;
        Debug.Log("hello" + childEnemyStatsScript.alive);

        HeadObject.SetActive(false);
        EnemyWormBody.SetActive(true);

        GetComponent<EnemyPatrolGround>().stop = false;


        startTime = Time.time;
        if (!childEnemyStatsScript.alive){
            Destroy(gameObject, 0.5f);
        }
    }

    void Die()
    {
        Destroy(gameObject, 0.5f);
    }

}
