using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchScore : MonoBehaviour
{
    public Text t;
    void Start()
    {
        float g = GameObject.Find("GameData").GetComponent<GameData>().temp;
        t.text = $"Your score: {g:F0}";
    }
}
