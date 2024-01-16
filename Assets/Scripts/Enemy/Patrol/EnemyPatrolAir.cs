using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class EnemyPatrolAir : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;

    public GameObject player;


    public float distancethreshold;

    public float speed;
    public float visionRange;
    public float chaseDistance;
    bool facingLeft;
    public float speed_patrol;
    //public LayerMask platformLayer;

    public GameObject gameManager;

    public bool isGhost = false;

    public float stopDistance;      // How close we get before moving to the next patrol point
    public Vector2[] patrolPoints;  // List of patrol points we will go between

    private int currentPoint = 0;

    private bool died = false;


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

        if (gameManager == null){
            gameManager = GameObject.FindGameObjectWithTag("GameController");
        }

        target = gameManager.GetComponent<GameManager>().getPlayerTransform();



    }

    void Update()
    {
         if(target == null){

            died = true;
            
            FindPlayer();

            Debug.Log("is null");
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

       

        Debug.Log("Chasing");

        
        Vector3 direction = target.position - transform.position;
        Vector3 direction_normalized = direction.normalized;


        float distance = direction.magnitude;

        // Adjust speed based on distance (optional)
        float adjustedSpeed = speed * Mathf.Clamp01(distance / distancethreshold);

        // Calculate force based on adjusted speed
        Vector2 force = direction.normalized * adjustedSpeed;

        

        

        Debug.Log(died);
        if(died){
            
            rb.AddForce(force * new Vector2(10f,10f), ForceMode2D.Force);
            
            
        }
        else{
            rb.AddForce(force, ForceMode2D.Force );
        }


        


        //rb.velocity = new Vector2(target.position.x * 0.01f, target.position.y * 0.01f);

        //Vector2 direction = (target.position - transform.position).normalized;
        // Adjust speed based on distance (optional, you can remove this if not needed)
        //float adjustedSpeed = speed * Mathf.Clamp01(Vector2.Distance(transform.position, target.position));
        // Set the rigidbody's velocity directly
        //rb.velocity = direction * adjustedSpeed;

        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

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
