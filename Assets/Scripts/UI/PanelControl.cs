using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public Text[] ranks;

    void OnEnable()
    {
        GameData g = GameObject.Find("GameData").GetComponent<GameData>();
        int min = Mathf.Min(3,g.list.Count);
        for (int i = 0; i < min; i++)
        {
           ranks[i].text = (i+1) +"."+ g.list[i].name + " - " + g.list[i].score; 
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
