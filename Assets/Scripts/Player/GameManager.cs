using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lifeNumber;

    [SerializeField] Text life;

    [SerializeField] GameObject testPrefab;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    void OnEnable()
    {
        dieChannel.AddListener(Die);
        positionChannel.AddListener(NewLife);
        life.text = lifeNumber.ToString("D2");
    }

    void Die()
    {
        lifeNumber -= 1;
        life.text = lifeNumber.ToString("D2");
        Debug.Log("-1");
        if (lifeNumber <= 0)
        {
            GameOver();
        }
    }

    void NewLife(Vector3 position)
    {
        if (lifeNumber > 0)
        {
            Instantiate(testPrefab, position, Quaternion.identity);
        }
        
    }

    void GameOver()
    {
        Debug.Log("game over");
    }
}
