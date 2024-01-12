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
        statusShow.text = list[n].ToString();
    } 
}
