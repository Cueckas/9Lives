using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimator : MonoBehaviour

{
    // Start is called before the first frame update
    
    private Animator _anim;
    //TransparencyChanger script = frozenScreen.GetComponent<TransparencyChanger>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
            HandleExplosion();
    }


    void HandleExplosion()
    {
        //_anim.SetTrigger(ExplodeKey);
          
    }

    public struct FrameInput
    {
        public bool AttackHeld;
        public bool Attack;
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }



      //private static readonly int ExplodeKey = Animator.StringToHash("Explode");

}
