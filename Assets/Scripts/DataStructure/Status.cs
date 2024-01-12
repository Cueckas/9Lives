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

    public float attackRate;

    public int id;

    public Status(float timeLife, float hp, float speed, float jumpForce, float attack, float attackSpeed, float attackRate){
        this.timeLife = timeLife;
        this.hp = hp;
        this.speed = speed;
        this.jumpForce = jumpForce;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.attackRate = attackRate;
    }

    public Status(){}

    public Status Copy()
    {
        return new Status(timeLife,hp, speed,jumpForce, attack, attackSpeed,attackRate);
    }

    public void RandomAllStatus(float min, float max)
    {
        this.timeLife *= UnityEngine.Random.Range(min,max);
        this.hp *= UnityEngine.Random.Range(min,max);
        this.speed *= UnityEngine.Random.Range(min,max);
        this.jumpForce *= UnityEngine.Random.Range(min,max);
        this.attack *= UnityEngine.Random.Range(min,max);
        this.attackSpeed *= UnityEngine.Random.Range(min,max);
        this.attackRate *= UnityEngine.Random.Range(min,max);
    }

    public void RandomSingleStatus(float min, float max, int status)
    {
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
        }else if (status == 6)
        {
            this.attackRate *= UnityEngine.Random.Range(min,max);
        }        
    }

    public override String ToString()
    {
        return $"Time Life: {timeLife:F0}\n" +
               $"HP: {hp:F0}\n" +
               $"Speed: {speed:F0}\n" +
               $"Jump Force: {jumpForce:F0}\n" +
               $"Attack: {attack:F0}\n" +
               $"Attack Speed: {attackSpeed:F0}\n" +
               $"Attack Rate: {attackRate:F0}";
    }
}
