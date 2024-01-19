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


#region knockback
    public bool hitPlayer = false;
    public bool resume = true;
    public Vector2 collisionVector; 
    public float knockbackForce = 1.1f;
#endregion


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);

        if(isGhost){

            Physics2D.IgnoreLayerCollision(8, 3, true);
        }

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

        }

    }

    void FixedUpdate(){

        if(target != null && resume && !hitPlayer){   

            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer < chaseDistance)
            {
                if(target.gameObject.GetComponent<PlayerManager>().isYoung == true || (target.gameObject.GetComponent<PlayerManager>().isYoung == false && isGhost)){
                    Chase();
                }
                else if(!isGhost){

                    Patrol();
                }
                
            }
            else if(!isGhost)
            {   
                Patrol();
            }

        } 
        else if(hitPlayer){
            resume = false;
            makeKnockback();
        }
    }

    private void makeKnockback()
    {


            
            Vector2 knockbackDirection = ((Vector2)transform.position - collisionVector).normalized;

            //for horizontal enemies
            //knockbackDirection.y = 0f;

            Debug.Log(knockbackDirection);

            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;
            //Debug.Log(knockbackDirection);
            // Apply knockback force in the opposite direction of the enemy
            rb.AddForce(knockbackForceVector, ForceMode2D.Impulse);

            hitPlayer = false;
            Invoke("resumeMovement", 0.5f);
    }

       private void resumeMovement(){

        resume = true;
        

    }

    
    void Chase()
    {   


        
        Vector3 direction = target.position - transform.position;
        Vector3 direction_normalized = direction.normalized;


        float distance = direction.magnitude;

        // Adjust speed based on distance (optional)
        float adjustedSpeed = speed * Mathf.Clamp01(distance / distancethreshold);

        // Calculate force based on adjusted speed
        Vector2 force = direction.normalized * adjustedSpeed;

        

        

        //Debug.Log(died);
        if(died){
            
            rb.AddForce(force * new Vector2(1.5f,1.5f), ForceMode2D.Force);
            
            
        }
        else{
            rb.AddForce(force, ForceMode2D.Force );
        }

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

        if (direction.x > 0 && facingLeft)  
        {
            Flip();
        }
        else if (direction.x < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingLeft = !facingLeft;
    }



}
