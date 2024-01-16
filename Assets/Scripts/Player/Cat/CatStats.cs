using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CatStats : MonoBehaviour
{

    [SerializeField] public float timeLife;

    [SerializeField] public int hp;

    [SerializeField] public float speed;

    [SerializeField] public float jumpForce;

    [SerializeField] public float attack;

    [SerializeField] public float attackSpeed;

    [SerializeField] public float attackRate;

    [SerializeField] NumberEventChannel timerEvent;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;

    public GenerateNextGeneration gg;

    public HealthBar lifeBar;
    public Text lifeCounter;

    private int curHP;

    private float invicibleTime = 0.4f;
    private bool isInvicible = false;


    void Start()
    {
        timerEvent.Broadcast(timeLife);
        dieChannel.AddListener(Die);
        
        if (lifeCounter != null)
        {
            lifeBar.SetMaxHealth(hp);
            lifeCounter.text = $"{hp}/{hp}";
            gg.setFather(GetStatus());
        }
        curHP = hp;
        
    }
    void FixedUpdate()
    {
        if (isInvicible)
        {
            invicibleTime -= Time.fixedDeltaTime;
            if (invicibleTime <= 0)
            {
                isInvicible = false;
                invicibleTime = 0.4f;
            }
        }
        
    }

    void Die(){
        dieChannel.RemoveListener(Die);
        positionChannel.Broadcast(transform.GetChild(2).position);
        Destroy(gameObject);
    }

    void ChangeStatus(Status s)
    {
        this.timeLife = s.timeLife;
        this.hp = (int)s.hp;
        this.speed = s.speed;
        this.jumpForce = s.jumpForce;
        this.attack = s.attack;
        this.attackSpeed = s.attackSpeed;
        this.attackRate = s.attackRate;
    }

    public void Setup(GenerateNextGeneration gg, HealthBar lifeBar, Text lifeCounter, Status s)
    {
        ChangeStatus(s);
        gg.setFather(GetStatus());
        lifeBar.SetMaxHealth(hp);
        lifeCounter.text = $"{hp}/{hp}";
    }

    public Status GetStatus()
    {
        return new Status(timeLife,hp, speed,jumpForce, attack, attackSpeed,attackRate);
    }

    public void TakingDamage(int damage)
    {
        if (!isInvicible)
        {
            curHP -= damage;
            lifeCounter.text = $"{curHP}/{hp}";
            lifeBar.SetHealth(curHP);
            if (curHP <= 0)
            {
                dieChannel.Broadcast();
            }else
            {
                isInvicible = true;
            }
        }
        
    }
}
