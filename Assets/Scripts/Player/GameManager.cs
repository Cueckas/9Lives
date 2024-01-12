using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int lifeNumber;

    [SerializeField] Text life;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] GameObject deathmenu;

    [SerializeField] GameObject choosemenu;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;

    public GameObject main_camera;

    private bool isPaused = false; 

    public GameObject curPlayer;

    private List<Status> kittens; 
    private Vector3 DiePosition;
    void OnEnable()
    {
        dieChannel.AddListener(Die);
        positionChannel.AddListener(DiePos);
        kittens = new List<Status>();
        lifeNumber = kittens.Count;
        life.text = lifeNumber.ToString("D2");
    }

    void Die()
    {
        lifeNumber -= 1;
        life.text = lifeNumber.ToString("D2");
        if (lifeNumber >=1)
        {
            toggleDeathMenu();
        }
        
        if (lifeNumber < 0)
        {
            GameOver();
        }
    }

    public void NewGen(Status kitten, GenerateNextGeneration gg)
    {
        //cria novo gato e guarda na lista
        kittens.Add(kitten);
        lifeNumber = kittens.Count;
        life.text = lifeNumber.ToString("D2");
    }

    public void toggleDeathMenu()
    {
        Pause();
        deathmenu.SetActive(isPaused);  
    }

    public void togglechooseMenu()
    {
        Pause();
        choosemenu.SetActive(isPaused);
    }

 
    void DiePos(Vector3 position)
    {
        if (lifeNumber >= 0)
        {
            DiePosition = position;
            GameObject g = (GameObject) Resources.Load("Prefabs/Test_prefab");
            Instantiate(g, position, Quaternion.identity);
            if (lifeNumber == 0)
            {
                Respawn(0);
            }
        }
    }

    public void Respawn(int id)
    {
        Debug.Log(1237892137);
        GameObject g1 = (GameObject) Resources.Load("Prefabs/Player");
        
            //position will be a checkpoint
        GameObject player = Instantiate(g1, new Vector3 (DiePosition.x,DiePosition.y + 0.5f, DiePosition.z), Quaternion.identity);
        curPlayer = player;
        GameObject child = player.transform.GetChild(2).gameObject;
        main_camera.GetComponent<FollowObject>().changeTarget(child.transform);
        player.GetComponent<CatStats>().changeStatus(kittens[id]);
        kittens.RemoveAt(id);
    }

    void GameOver()
    {
        Debug.Log("game over");
    }

    public void Pause(){
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public List<Status> GetRandomKittens()
    {
        List<Status> result = new List<Status>();
        if(lifeNumber == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                Status s = kittens[i];
                s.id = i;
                result.Add(s);
            }
        }else
        {
            int check=-1;
            for (int i = 0; i < 2; i++)
            {
                int p = UnityEngine.Random.Range(0,lifeNumber);
                if (i == 1 && p == check)
                {
                    p = (p-1)%lifeNumber;
                }
                Status s = kittens[p];
                s.id = p;
                result.Add(s);
                check = p;
            }
        }
        return result;
    }

    public List<Status> GetKittenList()
    {
        List<Status> result = new List<Status>();
        result.Add(curPlayer.GetComponent<CatStats>().GetStatus());
        result.AddRange(kittens);
        return result;
    }
}
