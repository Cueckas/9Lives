using UnityEngine;

public class Enemy_WormHeadSpawn : MonoBehaviour
{
    public GameObject WormHead;
    //public Transform parentObject = transform.parent

    public float damage;

    //bool
    private bool spawnedWormHead = false;

    public float spawnCooldown = 3f; // Cooldown time in seconds

    //spawn timers
    private float cooldownTimer = 1.8f;

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
    private bool referenceAlive;
    
    private GameObject referenceObject;
    private EnemyPatrolGround patrolScript;
    private EnemyStats enemyStatsScript;
    private GameObject reference;
    void Start()
    {
        patrolScript = GetComponent<EnemyPatrolGround>();
        enemyStatsScript = GetComponent<EnemyStats>();


    }

    void Update()
    {
        {
            Debug.Log("player detected ");
            //find spawnedWorm
            if (!spawnedWormHead)
            {
                cooldownTimer = spawnCooldown;
                // Start or continue charging
                Debug.Log("eNTERED UPDATE KEY PRSSED");
                Spawn();
                spawnedWormHead = true;
            }

            if (spawnedWormHead )
            {
                cooldownTimer -= Time.deltaTime;
                float elapsedTime = Time.time - startTime;
                Debug.Log("Worm is alive");

                if (enemyStatsScript.alive && referenceAlive)
                {
                    reference.transform.position += new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
                }

            }

            if (cooldownTimer <= 0f)
            {
                spawnedWormHead = false;

            }

        }

    }
    void Spawn()
    {
      if (WormHead != null)
         {

          Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y +1f, transform.position.z);
          reference = Instantiate(WormHead, spawnPosition, transform.rotation);

          startTime = Time.time;
          Destroy(reference, 0.5f);
          }

        }

    void Die()
    {
        // Add any death-related logic here (e.g., play death animation, destroy GameObject, etc.)
        referenceAlive = false;
        Destroy(reference);
    }


}
