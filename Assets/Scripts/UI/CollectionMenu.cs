using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;
    
    private List<Status> list;

    public Text statusShow;

    public Text buffStat;
    public GameObject[] stars;
    void OnEnable()
    {
        list = gm.GetKittenList();
        Transform child =this.transform.GetChild(0);
        for (int i = 0; i < list.Count; i++)
        {
            child.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnClick(int n)
    {
        DisEnableStars();
        buffStat.text = "Buff Stat: "+ list[n].t;
        statusShow.text = list[n].ToString();
        for (int i = 0; i < list[n].buffRange; i++)
        {
            stars[i].SetActive(true);
        }
    } 

    public void DisEnableStars()
    {
        foreach (GameObject item in stars)
        {
            item.SetActive(false);
        }
    }
}
