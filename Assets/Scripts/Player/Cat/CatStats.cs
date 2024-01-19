using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CatStats : MonoBehaviour
{

    [SerializeField] public float timeLife;

    [SerializeField] public int hp;

    [SerializeField] public float speed;

    [SerializeField] public float attack;

    [SerializeField] NumberEventChannel timerEvent;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;
    public GameObject crutch;
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
        oldAgeEventChannel.AddListener(Old);
        curHP = hp;
        isInvicible = true;
        invicibleTime = 1.0f;
        if (GameObject.Find("PickBomb") == null)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (GameObject.Find("PickScythe") == null)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        try
        {
            lifeBar.SetMaxHealth(hp);
            lifeCounter.text = $"{hp}/{hp}";
            gg.setFather(GetStatus());
        }
        catch (NullReferenceException)
        {
        }
        
    }
    void FixedUpdate()
    {
        //Debug.Log(gameObject.GetComponent<PlayerController>() == null);
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
    void Old()
    {
        crutch.SetActive(true);
    }

    void Die(){
        dieChannel.RemoveListener(Die);
        positionChannel.Broadcast(transform.GetChild(2).position);
        oldAgeEventChannel.RemoveListener(Old);
        Destroy(gameObject);
    }

    void ChangeStatus(Status s)
    {
        this.timeLife = s.timeLife;
        this.hp = (int)s.hp;
        curHP = hp;
        this.speed = s.speed;
        this.attack = s.attack;
    }

    public void Setup(GenerateNextGeneration gg, HealthBar lifeBar, Text lifeCounter, Status s)
    {
        this.lifeBar = lifeBar;
        this.lifeCounter = lifeCounter;
        ChangeStatus(s);
        gg.setFather(GetStatus());
        lifeBar.SetMaxHealth(hp);
        lifeCounter.text = $"{hp}/{hp}";
    }

    public Status GetStatus()
    {
        return new Status(timeLife,hp, speed, attack);
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
