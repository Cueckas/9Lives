using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lifeNumber;

    [SerializeField] GameObject testPrefab;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    void OnEnable()
    {
        dieChannel.AddListener(Die);
        positionChannel.AddListener(NewLife);
    }

    void Die()
    {
        lifeNumber -= 1;
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
