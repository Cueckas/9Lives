using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNextGeneration : MonoBehaviour
{
    Status catOne;

    Status catTwo;

    int motherType;

    Status father;

    public Text buttonOne;
    public Text buttonTwo;
    public GameManager gm;
    void OnEnable(){
        catOne = father.Copy();
        catTwo = father.Copy();
        catOne.RandomAllStatus(0.9f, 1.1f);
        catTwo.RandomAllStatus(0.9f, 1.1f);
        //generate the status for cats
        //By mothertype will generate children with some status more powerfull

        catOne.RandomSingleStatus(1.1f,1.3f,motherType); //maybe 0 is more time life
        catTwo.RandomSingleStatus(1.1f,1.3f,motherType);

        buttonOne.text = catOne.ToString();
        buttonTwo.text = catTwo.ToString();
    }

    public void Choose(int n){
        if (n == 0)
        {
            gm.NewGen(catOne,this);
        }else if(n == 1)
        {
            gm.NewGen(catTwo,this);
        }
        gm.togglechooseMenu();
    }

    public void setFather(Status father)
    {
        this.father = father;
    }

    public void SetMother(int mother)
    {
        this.motherType = mother;
        //gm.togglechooseMenu();    //apresenta o menu de escolha

        //auto give next gen
        MakeNextGen();
        gm.NewGen(catOne,this);
    }

    public void MakeNextGen()
    {
        catOne = father.Copy();
        catOne.RandomAllStatus(0.9f, 1.1f);
        catOne.RandomSingleStatus(1.1f,1.3f,motherType);
    }
}
