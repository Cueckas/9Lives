using System;
using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;

        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;
        
        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;
        public event Action Walking;
        public event Action Scythe;

        private float timer = 0f;

        private float maxWallTime = 0.5f;

        private bool activateWallClimb = false;

        private bool expiredWallTime = false;



    #region knockback stuff
    public float knockbackForce = 5f;

    //private Rigidbody2D playerRb;

    #endregion

        #endregion

        private float _time;
        private bool _walking;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            GatherInput();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (_stats.SnapInput)
            {  
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }

        private void FixedUpdate()
        {
            CheckCollisions();


            if(!expiredWallTime){
                HandleWallClimbing();
                Debug.Log("can make a wall climb again");
            }
            
            HandleJump();
            HandleDirection();       
            HandleGravity();
            
            HandleMove(); // handles Move Animations
            ApplyMovement();
            HandleScytheAttack();

        }


        public void HandleMove()
        {
            if (_frameVelocity.x > 0 && !_frameInput.Attack) { 
                //_walking = true;
                Walking?.Invoke();

            }
   
        }

        public void HandleScytheAttack()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Scythe?.Invoke();
            }

        }

        

        #region Collisions

        private float _frameLeftGrounded = float.MinValue;
        public bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        
    void OnCollisionEnter2D(Collision2D collision){

         if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Vector2 rbVelocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
            Vector2 knockbackDirection = rbVelocity.normalized + ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;

            Vector2 knockbackForceVector = knockbackDirection * knockbackForce;

            //Debug.Log(knockbackDirection);
            // Apply knockback force in the opposite direction of the enemy
            _rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);// Do other things, e.g., damage the player, play sound, etc.

                    // Add the knockback force to the frame velocity directly
           // _frameVelocity += knockbackForceVector;

            // Log the frame velocity with knockback applied
            //Debug.Log("Frame Velocity with Knockback: " + _frameVelocity);

            //Debug.Log("Player collided with an enemy!");


        }

    }
        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {

            
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();

            expiredWallTime = false;
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (isWallMove && activateWallClimb)
            {
                return;
            }
            if (_grounded && _frameVelocity.y <= 0f )
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif


    //New Part 
    bool isLeftWall, isRightWall;
    public Vector3 wallOffset;
    public LayerMask wallLayer;
    public bool isWallMove;
    void Wallcheck()
    {
        isLeftWall = Physics2D.OverlapCircle(transform.position - wallOffset, 0.1f, wallLayer);
        isRightWall = Physics2D.OverlapCircle(transform.position + wallOffset, 0.1f, wallLayer);

        
        
        isWallMove = isLeftWall || isRightWall;

        if(isWallMove){

            Debug.Log("isLeftWall: " + isLeftWall);
            Debug.Log("isRightWall: " + isLeftWall);
        }
    }

    void HandleWallClimbing()
    {

        
        

        Wallcheck();
        if (isWallMove)
        {

            if(!activateWallClimb){

                activateWallClimb = true;
                timer = maxWallTime;
            }
            else{

                timer -= Time.deltaTime;
                if(timer <= 0){
                    activateWallClimb= false;
                    isWallMove = false;
                    expiredWallTime = true;
                }
            }

            if(activateWallClimb){

                _grounded = isWallMove;
                float playerInput = Input.GetAxis("Vertical");
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

        }
    }

    void WallClimb()
    {
        _frameVelocity.y = 1;
    }

    void WallSlide()
    {
        _frameVelocity.y = _stats.GroundingForce;
    }

    void WallGrab()
    {
        _frameVelocity.y = 0;
    }
    }

    public struct FrameInput
    {
        public bool AttackHeld;
        public bool Attack;
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
        public bool Scythe;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        //public event Action Walking;
        public event Action Walking;

        public event Action Scythe;

        public Vector2 FrameInput { get; }
    }
}