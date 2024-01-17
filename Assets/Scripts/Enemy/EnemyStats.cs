using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{   
    public float fullHp;
    public float attackPower;
    //public LayerMask platform;
    //public gameObject Player;

    private float currentHp;
    public bool alive;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = fullHp;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Add your enemy behavior code here
    }

    // Called when a 2D collision occurs
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("object collided: " + collision.otherCollider.GetType());
        Debug.Log("Collision gameobject: " + collision.gameObject.name);

        // Check if the colliding object has a Rigidbody2D component
        Physics2D.IgnoreLayerCollision(8, 7, true);

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        //bool grounded = collision.gameObject.GetComponent<PlayerController>();
        //PhysicsMaterial2D physicsMaterial2D = collision.sharedMaterial;
        //Collider2D collider2D = collision.collider;

        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        PhysicsMaterial2D colliderMaterial = boxCollider.sharedMaterial;

        //PhysicsMaterial2D ownColliderMaterial =  boxCollider.;
        if (playerRigidbody != null && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            // Check if the player is moving downwards (jumping on top)
            if (collision.otherCollider.GetType().Name == "BoxCollider2D" ) //&& colliderMaterial!=null)
            {
                
                // Player is moving downward, so damage the enemy
                TakeDamage(1); // You can adjust the damage amount as
                Debug.Log("Should Die");
            }

        }

        Debug.Log("cOLLISIOn detected");

    }

    
    public void TakeDamage(float amount)
    {
        currentHp -= amount;

        // Check if the enemy's health is less than or equal to 0
        if (currentHp <= 0)
        {   
            Die();
        }
    }

    void Die()
    {
        // Add any death-related logic here (e.g., play death animation, destroy GameObject, etc.)
        alive = false;
        Destroy(gameObject,0.2f);
    }


}   