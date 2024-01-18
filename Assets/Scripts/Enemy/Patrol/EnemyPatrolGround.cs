using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolGround : MonoBehaviour
{
    // ------------------------------------------------
    // Public variables, visible in Unity Inspector
    // Use these for settings for your script
    // that can be changed easily
    // ------------------------------------------------
    public float forceStrength;     // How fast we move
    public float stopDistance;      // How close we get before moving to next patrol point
    public Vector2[] patrolPoints;  // List of patrol points we will go between
    public float damage;
    public Transform target;
    public float visionRange;

    public float followSpeed = 1f;

    public float fleeSpeed = 2f;

    public bool isRat = false;

    public bool stop = false;
    private bool facingLeft;

    private GameObject gameManager;

    private bool died = false;


    // ------------------------------------------------
    // Private variables, NOT visible in the Inspector
    // Use these for tracking data while the game
    // is running
    // ------------------------------------------------
    private int currentPoint = 0;       // Index of the current point we're moving towards
    private Rigidbody2D ourRigidbody;   // The rigidbody attached to this object
    private float ownHp;
    public bool detected = false;
    private Vector2 originalPosition;


#region 
    public bool hitPlayer = false;
    public bool resume = true;
    public Vector2 collisionVector; 
    public float knockbackForce = 1.1f;
#endregion


    // ------------------------------------------------
    // Awake is called when the script is loaded
    // ------------------------------------------------
    void Awake()
    {
        // Get the rigidbody that we'll be using for movement
        
        ourRigidbody = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        //ourRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        
    }
    


    void Update(){

        if(target == null){

            FindPlayer();
            died = true;

        }





         

 
        Detected();
    }

    private void FindPlayer()
    {
        if (gameManager == null){
            gameManager = GameObject.FindGameObjectWithTag("GameController");
        }

        target = gameManager.GetComponent<GameManager>().getPlayerTransform();
    }

    void FixedUpdate()
    {

        if(!stop && resume && !hitPlayer){

            bool? is_young = target.gameObject.GetComponent<PlayerManager>().isYoung;

            if(detected){

                if(is_young == true){
                    FollowTarget();
                }
                else{
                    if(isRat){

                        if(is_young == false)
                            FleeTarget();  
                        else
                            Patrol();
                    }
                    else{
                        Patrol();
                    }
                    
                }
            }
            else{
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
            knockbackDirection.y = 0f;

            Debug.Log(knockbackDirection);

            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;
            //Debug.Log(knockbackDirection);
            // Apply knockback force in the opposite direction of the enemy
            ourRigidbody.AddForce(knockbackForceVector, ForceMode2D.Impulse);

            hitPlayer = false;
            Invoke("resumeMovement", 0.5f);
    }

    private void resumeMovement(){

        resume = true;
        

    }

    void Patrol()
{
    float distance = (patrolPoints[currentPoint] - (Vector2)transform.position).magnitude;

    if (!detected || (isRat && target.gameObject.GetComponent<PlayerManager>().isYoung == null))
    {
        // If we are closer to our target than our minimum distance...
        if (distance <= stopDistance)
        {
 
            currentPoint = currentPoint + 1;

            if (currentPoint >= patrolPoints.Length)
            {
 
                currentPoint = 0;
            }
        }

        // Now, move in the direction of our target

        // Get the direction
        Vector2 direction = (patrolPoints[currentPoint] - (Vector2)transform.position).normalized;

        // Calculate custom direction vector for x-axis movement only
        Vector2 customDirection = new Vector2(patrolPoints[currentPoint].x - transform.position.x, 0f).normalized;

        // Set y-component to zero, making the movement only along the x-axis
        direction.y = 0f;

        ourRigidbody.velocity = new Vector2(customDirection.x * forceStrength, ourRigidbody.velocity.y);

        // Move in the correct direction with the set force strength
        //ourRigidbody.velocity = new Vector2(direction.x * forceStrength ,ourRigidbody.velocity.y);

        if (direction.x > 0 && facingLeft)  
        {
            Flip();
        }
        else if (direction.x < 0 && !facingLeft)
        {
            Flip();
        }
    }

    
}


    void Detected(){


        if(target.gameObject.GetComponent<PlayerManager>().isYoung == false){

            if (Vector2.Distance(transform.position, target.position) < visionRange - 1.5f){
                detected = true;
            }

            else {
                detected = false;
            
            }

        }else{
            
            if (Vector2.Distance(transform.position, target.position) < visionRange){
            detected = true;
        }

        else {
            detected = false;
            
        }

        }




    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingLeft = !facingLeft;
    }

    void FollowTarget()
    {   
    // Calculate direction to the target
        Vector2 direction = (target.position - transform.position).normalized;

        if(died){

            ourRigidbody.velocity = new Vector2(direction.x * followSpeed, ourRigidbody.velocity.y) * new Vector2(1f,1f);
        }
        else{

            ourRigidbody.velocity = new Vector2(direction.x * followSpeed, ourRigidbody.velocity.y);
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


    void FleeTarget()
{   
    // Calculate direction away from the target
    Vector2 direction = -(target.position - transform.position).normalized;

    if (died)
    {
        ourRigidbody.velocity = new Vector2(direction.x * fleeSpeed, ourRigidbody.velocity.y) * new Vector2(10f, 10f);
    }
    else
    {
        ourRigidbody.velocity = new Vector2(direction.x * fleeSpeed, ourRigidbody.velocity.y);
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

}