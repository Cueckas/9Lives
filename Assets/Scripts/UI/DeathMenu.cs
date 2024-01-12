using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Text buttonOne;
    public Text buttonTwo;

    public GameManager gm;

    private List<Status> l;

    void OnEnable(){
        l = gm.GetRandomKittens();
        buttonOne.text = l[0].ToString();
        buttonTwo.text = l[1].ToString();
    }

    public void SelectKitten(int n)
    {
        gm.Respawn(n==0?l[0].id:l[1].id);
        gm.toggleDeathMenu();
    }
}
