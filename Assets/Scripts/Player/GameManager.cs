using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    

    [SerializeField] Text life;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] GameObject deathmenu;

    [SerializeField] GameObject choosemenu;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;

    [SerializeField] VoidEventChannel coinChannel;

    public GameObject main_camera;

    public GameObject curPlayer;

    public HealthBar lifeBar;

    public GenerateNextGeneration gg;
    public Text lifeCounter;
    public Text coinCounter;

    public TextMeshProUGUI agetext;
    public static event System.Action OnPlayerRespawn;

    bool isPaused = false; 
    int lifeNumber;
    int coinNumber = 0;
    List<Status> kittens; 
    Vector3 DiePosition;
    void OnEnable()
    {
        dieChannel.AddListener(Die);
        coinChannel.AddListener(CatchCoin);
        positionChannel.AddListener(DiePos);

    
        youngEventChannel.AddListener(youngHandler);
        middleAgeEventChannel.AddListener(middle_ageHandler);
        oldAgeEventChannel.AddListener(oldHandler);

        kittens = new List<Status>();
        lifeNumber = kittens.Count;
        life.text = lifeNumber.ToString("D2");
    }

    private void middle_ageHandler()
    {
        agetext.text = "Adult Cat";
    }

    private void oldHandler()
    {
         agetext.text = "Old Tomcat";
    }

    private void youngHandler()
    {
        agetext.text = "Young Kitten";
    }


    void Die()
    {
        lifeNumber -= 1;
        life.text = lifeNumber.ToString("D2");
        toggleDeathMenu();
        
        if (lifeNumber < 0)
        {
            GameOver();
        }
    }

    public void NewGen(Status kitten, GenerateNextGeneration gg)
    {
        //cria novo gato e guarda na lista
        kittens.Add(kitten);
        kittens.Add(kitten.Copy());
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
            GameObject g = (GameObject) Resources.Load("Prefabs/Test_prefab");
            Instantiate(g, position, Quaternion.identity);
        }
    }

    void CatchCoin()
    {
        coinNumber ++;
        coinCounter.text = coinNumber.ToString("D2");
    }

    public void Respawn(int id)
    {
        GameObject g1 = (GameObject) Resources.Load("Prefabs/Player");
        
            //position will be a checkpoint
        GameObject player = Instantiate(g1, new Vector3 (DiePosition.x,DiePosition.y + 0.5f, DiePosition.z), Quaternion.identity);
        curPlayer = player;
        GameObject child = player.transform.GetChild(2).gameObject;


        main_camera.GetComponent<FollowObject>().changeTarget(child.transform);

        player.GetComponent<CatStats>().Setup(gg,lifeBar,lifeCounter,kittens[id]);
        kittens.RemoveAt(id);
    }

    public Transform getPlayerTransform(){

        if(curPlayer == null){

            //Debug.Log(curPlayer);
            return null;
        }

        else{
            return curPlayer.transform.GetChild(2);
        }

        
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
        if(lifeNumber == 0)
        {
            Status s = kittens[0];
            s.id = 0;
            result.Add(s);
        }else if(lifeNumber == 1)
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

    internal void setSavePoint(Vector3 position)
    {
        DiePosition = position;
    }

     public static void PlayerRespawned()
    {
        OnPlayerRespawn?.Invoke();
    }

    public void SetResult()
    {
        float score = coinNumber * Mathf.Max(kittens.Count,1);
        GameObject.Find("GameData")?.GetComponent<GameData>().Add(score);
    }
}
