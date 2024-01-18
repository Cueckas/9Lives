using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Status 
{
    public float timeLife;

    public float hp;

    public float speed;

    public float jumpForce;

    public float attack;

    public float attackSpeed;

    public int id;

    private List<int> parents;

    private bool buff0, buff1, buff2, buff3, buff4, buff5;

    public Status(float timeLife, float hp, float speed, float jumpForce, float attack, float attackSpeed){
        this.timeLife = timeLife;
        this.hp = hp;
        this.speed = speed;
        this.jumpForce = jumpForce;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        parents = new List<int>();
    }

    public Status(){}

    public Status Copy()
    {
        Status s = new Status(timeLife,hp, speed,jumpForce, attack, attackSpeed);
        s.AddRange(parents);
        return s;
    }

    void AddRange(List<int> p)
    {
        parents.AddRange(p);
    } 

    public void RandomAllStatus(float min, float max)
    {
        this.timeLife *= UnityEngine.Random.Range(min,max);
        this.hp *= UnityEngine.Random.Range(min,max);
        this.speed *= UnityEngine.Random.Range(min,max);
        this.jumpForce *= UnityEngine.Random.Range(min,max);
        this.attack *= UnityEngine.Random.Range(min,max);
        this.attackSpeed *= UnityEngine.Random.Range(min,max);
    }

    void Mutate()
    {
        if (buff0 && buff1)
        {
            this.timeLife *= UnityEngine.Random.Range(1.1f,1.3f);
            this.hp *= UnityEngine.Random.Range(1.1f,1.3f);
        }
        if (buff2 && buff3)
        {
            this.speed *= UnityEngine.Random.Range(1.1f,1.2f);
            this.jumpForce *= UnityEngine.Random.Range(1.1f,1.2f);
        }
        if (buff4 && buff5)
        {
            this.attack *= 1.1f;
            this.attackSpeed *= UnityEngine.Random.Range(1.1f,1.2f);
        }
        if (buff2 && buff5)
        {
            this.speed *= UnityEngine.Random.Range(1.1f,1.2f);
            this.attackSpeed *= UnityEngine.Random.Range(1.1f,1.2f);
        }
        if (buff0 && (buff2 || buff3))
        {
            if (UnityEngine.Random.Range(0,2) == 0)
            {
                this.timeLife *= UnityEngine.Random.Range(0.8f,0.95f);
            }else
            {
                if (buff2 )
                {
                    this.speed *= UnityEngine.Random.Range(0.9f,0.95f);
                }else
                {
                    this.jumpForce *= UnityEngine.Random.Range(0.9f,0.95f);
                }
            }
        }
    }

    private void Check(int item)
    {
        if (item == 0)
        {
            buff0 = true;
        }else if (item == 1)
        {
            buff1 = true;
        }else if (item == 2)
        {
            buff2 = true;
        }else if (item == 3)
        {
            buff3 = true;
        }else if (item == 4)
        {
            buff4 = true;
        }else if (item == 5)
        {
            buff5 = true;
        }
    }

    public void RandomSingleStatus(float min, float max, int status)
    {
        parents.Add(status);
        Check(status);
        if (status == 0)
        {
            this.timeLife *= UnityEngine.Random.Range(min,max);
        }else if (status == 1)
        {
            this.hp *= UnityEngine.Random.Range(min,max);
        }else if (status == 2)
        {
            this.speed *= UnityEngine.Random.Range(min,max);
        }else if (status == 3)
        {
            this.jumpForce *= UnityEngine.Random.Range(min,max);
        }else if (status == 4)
        {
            this.attack *= UnityEngine.Random.Range(min,max);
        }else if (status == 5)
        {
            this.attackSpeed *= UnityEngine.Random.Range(min,max);
        }
        Mutate();       
    }

    public override String ToString()
    {
        return $"Time Life: {timeLife:F0}\n" +
               $"HP: {hp:F0}\n" +
               $"Speed: {speed:F0}\n" +
               $"Jump Force: {jumpForce:F0}\n" +
               $"Attack: {attack:F0}\n" +
               $"Attack Speed: {attackSpeed:F0}\n";
    }
}
