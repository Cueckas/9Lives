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

    public float attack;

    public int id;

    private List<int> parents;

    private bool[] buffs;

    public Status(float timeLife, float hp, float speed, float attack){
        this.timeLife = timeLife;
        this.hp = hp;
        this.speed = speed;
        this.attack = attack;
        parents = new List<int>();
        buffs = new bool[4];
    }

    public Status(){}

    public Status Copy()
    {
        Status s = new Status(timeLife,hp, speed, attack);
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
        this.attack *= UnityEngine.Random.Range(min,max);
    }

    void Mutate()
    {
        if (buffs[0] && buffs[1])
        {
            this.timeLife *= UnityEngine.Random.Range(1.1f,1.3f);
            this.hp *= UnityEngine.Random.Range(1.1f,1.3f);
        }
        if (buffs[2] && buffs[3])
        {
            this.speed *= UnityEngine.Random.Range(1.1f,1.2f);
            this.attack *= UnityEngine.Random.Range(1.1f,1.2f);
        }
        if (buffs[0] && (buffs[2] || buffs[3]))
        {
            if (UnityEngine.Random.Range(0,2) == 0)
            {
                this.timeLife *= UnityEngine.Random.Range(0.8f,0.95f);
            }else
            {
                if (buffs[2])
                {
                    this.speed *= UnityEngine.Random.Range(0.9f,0.95f);
                }else
                {
                    this.attack *= UnityEngine.Random.Range(0.9f,0.95f);
                }
                
            }
        }
    }

    private void Check(int item)
    {
        buffs[item] =true;
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
        }else if (status == 4)
        {
            this.attack *= UnityEngine.Random.Range(min,max);
        }
        Mutate();       
    }

    public override String ToString()
    {
        return $"Time Life: {timeLife:F0}\n" +
               $"HP: {hp:F0}\n" +
               $"Speed: {speed:F0}\n" +
               $"Attack: {attack:F0}\n";
    }
}
