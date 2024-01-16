using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public Vector3 wallOffset;
    public bool isLeftWall, isRightWall;
    public LayerMask wallLayer;
    public bool isWallMove;

    public enum WallState
    {
        wallGrab,
        wallSlide,
        wallClimb,
        wallJump,
        none
    }

    WallState ws;
    Rigidbody2D rb;

    void Start()
    {
        ws = WallState.none;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerInput = Input.GetAxis("Vertical");
        
        isWallMove = Wallcheck();

        if (isWallMove)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            //Debug.Log(playerInput);
            if (playerInput > 0)
            {
                WallClimb();
            }
            else if (playerInput < 0)
            {
                WallSlide();
            }
            else
            {
                WallGrab();
            }
        }
        else
        {
            ws = WallState.none;
            //rb.gravityScale = 3f;
        }
    }

    bool Wallcheck()
    {
        isLeftWall = Physics2D.OverlapCircle(transform.position - wallOffset, 0.1f, wallLayer);
        isRightWall = Physics2D.OverlapCircle(transform.position + wallOffset, 0.1f, wallLayer);
        return isLeftWall || isRightWall;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - wallOffset, 0.1f);
        Gizmos.DrawWireSphere(transform.position + wallOffset, 0.1f);
    }

    void WallGrab()
    {
        rb.velocity = Vector2.zero;
        ws = WallState.wallGrab;
    }

    void WallClimb()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10);
        ws = WallState.wallClimb;
    }

    void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -13);
        ws = WallState.wallSlide;
    }
}
