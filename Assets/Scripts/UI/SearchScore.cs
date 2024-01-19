using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchScore : MonoBehaviour
{
    public Text t;
    public InputField playerName;
    public KeyCode chargeKey;
    void Start()
    {
        float g = GameObject.Find("GameData").GetComponent<GameData>().temp.score;
        t.text = $"Your score: {g:F0}";
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Submit();
        }
    }

    public void Change()
    {
      GameObject.Find("GameData").GetComponent<GameData>().temp.name = playerName.text;  
    }

    public void Resyart()
    {
        Change();
        SceneManager.LoadScene(1);
    }

    public void Submit()
    {
        Change();
        SceneManager.LoadScene(0);
    }
}
