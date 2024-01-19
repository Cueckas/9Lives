using UnityEngine;
using TarodevController;
using System;
using TMPro; // Import the TarodevController namespace

public class PlayerManager : MonoBehaviour
{
    public ScriptableStats playerStats; // Reference to the ScriptableStats instance

    // Base attributes for the player
    private float baseScale = 1f;
    private float baseSpeed; // Base speed will be set from ScriptableStats
    private float baseJumpHeight; // Base jump height will be set from ScriptableStats
    private float baseAttackDamage = 2f;

    // Actual attributes of the player
    public float scale;
    public float speed;
    public float jumpHeight;
    public float attackDamage;

    // References to the event channels
    public VoidEventChannel youngEventChannel;
    public VoidEventChannel middleAgeEventChannel;
    public VoidEventChannel oldAgeEventChannel;

    public bool? isYoung = false;

    public bool normalSize = false;

    public bool isInSmallHole = false;

    public TextMeshPro agetext;

    CapsuleCollider2D [] colliders = null;


#region knockback stuff
    public float knockbackForce = 5f;

    private Rigidbody2D playerRb;

#endregion

    void Start()
    {
        // Initialize player attributes from ScriptableStats
        baseSpeed = playerStats.MaxSpeed;
        baseJumpHeight = playerStats.JumpPower;
        scale = baseScale;
        speed = baseSpeed;
        jumpHeight = baseJumpHeight;
        attackDamage = baseAttackDamage;

        // Subscribe to the events
        youngEventChannel.AddListener(BecomeYoung);
        middleAgeEventChannel.AddListener(BecomeMiddleAged);
        oldAgeEventChannel.AddListener(BecomeOld);

        colliders = GetComponents<CapsuleCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    public void BecomeYoung()
    {
        // Adjust player attributes for the young phase
        scale = baseScale * 0.8f;
        speed = baseSpeed * 1.2f;
        jumpHeight = baseJumpHeight * 1.2f;

        isYoung = true;

        //GetComponents<CapsuleCollider2D>()[0].
        //GetComponents<CapsuleCollider2D>()[1].enabled = true;
        ModifyColliderSize(GetComponent<CapsuleCollider2D>(),false);
        UpdatePlayerAppearance(true); // Update player's appearance and capabilities
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        
         if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("collision detected");
             // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            // Apply knockback force in the opposite direction of the enemy
            playerRb.velocity = knockbackDirection * knockbackForce;
            // Do other things, e.g., damage the player, play sound, etc.
           // Debug.Log("Player collided with an enemy!");


        }

    }

    public void BecomeMiddleAged()
    {
        // Adjust player attributes for the middle-aged phase
        scale = baseScale * 1.2f;
        speed = baseSpeed; // Keep base speed
        jumpHeight = baseJumpHeight; // Keep base jump height
        //GetComponents<CapsuleCollider2D>()[0].enabled = true;
        //GetComponents<CapsuleCollider2D>()[1].enabled = false;
        ModifyColliderSize(GetComponent<CapsuleCollider2D>(),true);
        UpdatePlayerAppearance(false); // Update player's appearance and capabilities

        isYoung = null;
        

    }

    public void BecomeOld()
    {
        // Adjust player attributes for the old age phase
        scale = baseScale;
        speed = baseSpeed * 0.8f;
        jumpHeight = baseJumpHeight * 0.8f;
        attackDamage = baseAttackDamage * 1.2f;
        UpdatePlayerAppearance(null); // Update player's appearance and capabilities

        isYoung = false;
       
    }

    public void UpdatePlayerAppearance(bool? young)
    {
        // This method updates the player's appearance and capabilities
        transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
        if(young == true){
            transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 0.2f,transform.GetChild(0).position.z);
        }
        else if(young == false && !isInSmallHole){
            transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.2f,transform.GetChild(0).position.z);
        }
        
        
        
         // Adjust the player's scale
        // Other relevant updates can be added here
    }

    void OnDestroy()
    {
        youngEventChannel.RemoveListener(BecomeYoung);
        middleAgeEventChannel.RemoveListener(BecomeMiddleAged);
        oldAgeEventChannel.RemoveListener(BecomeOld);
    }

    void ModifyColliderSize(CapsuleCollider2D collider, bool increase)
    {
        if (!increase)
        {
            // Set the new size of the capsule collider
            collider.size = collider.size/2;
            normalSize = false;
        }
        else if(!isInSmallHole){
            collider.size = collider.size*2;
            normalSize = true;
        }
    }


    public void increaseSize_OutOfHole(){

        if(isInSmallHole && !normalSize){
            GetComponent<CapsuleCollider2D>().size = GetComponent<CapsuleCollider2D>().size*2;
            isInSmallHole = false;
            //transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.2f,transform.GetChild(0).position.z);
            normalSize = true;
        }
        
    }

}
