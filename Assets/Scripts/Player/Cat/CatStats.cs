using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{

    [SerializeField] float timeLife;

    [SerializeField] float hp;

    [SerializeField] float speed;

    [SerializeField] float jumpForce;

    [SerializeField] float attack;

    [SerializeField] float attackSpeed;

    [SerializeField] float attackRate;

    [SerializeField] NumberEventChannel timerEvent;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;

    public GenerateNextGeneration gg;


    void Start()
    {
        timerEvent.Broadcast(timeLife);
        dieChannel.AddListener(Die);
        gg.setFather(this.GetStatus());
    }

    void Die(){
        dieChannel.RemoveListener(Die);
        positionChannel.Broadcast(transform.GetChild(2).position);
        Destroy(gameObject);
    }

    public void changeStatus(Status s)
    {
        this.timeLife = s.timeLife;
        this.hp = s.hp;
        this.speed = s.speed;
        this.jumpForce = s.jumpForce;
        this.attack = s.attack;
        this.attackSpeed = s.attackSpeed;
        this.attackRate = s.attackRate;
    }

    public Status GetStatus()
    {
        return new Status(timeLife,hp, speed,jumpForce, attack, attackSpeed,attackRate);
    }
}
