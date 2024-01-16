using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class EnemyPatrolAir : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    public float speed;
    public float visionRange;
    public float chaseDistance;
    bool facingLeft;
    public float speed_patrol;
    //public LayerMask platformLayer;

    public bool isGhost = false;

    public float stopDistance;      // How close we get before moving to the next patrol point
    public Vector2[] patrolPoints;  // List of patrol points we will go between

    private int currentPoint = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);

        FindPlayer();


    
    }

        void FindPlayer()
    {
        // Find the player object using the specified tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Check if the player object exists
        if (player != null)
        {
            Debug.Log("Player Found");
            target = player.transform;
        }
    }

    void Update()
    {
         if(target == null){
            FindPlayer();
        }

        else{   

            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer < chaseDistance)
            {
                Chase();
            }
            else
            {   
                if(!isGhost){
                Patrol();
                }
            }

        } 
    }

    void Chase()
    {   
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction.normalized * speed);

        if (direction.x > 0 && facingLeft)
        {
            Flip();
        }
        else if (direction.x < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void Patrol()
    {
        float distance = (patrolPoints[currentPoint] - (Vector2)transform.position).magnitude;

        if (distance <= stopDistance)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }

        Vector2 direction = (patrolPoints[currentPoint] - (Vector2)transform.position).normalized;
        rb.AddForce(direction * speed_patrol);
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingLeft = !facingLeft;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
        }
    }
}
