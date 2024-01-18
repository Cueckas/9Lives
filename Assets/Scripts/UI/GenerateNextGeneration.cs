using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GenerateNextGeneration : MonoBehaviour
{
    Status catOne;

    int motherType;

    Status father;

    public Text buttonOne;
    public Text buttonTwo;
    public GameManager gm;

    public void setFather(Status father)
    {
        this.father = father;
    }

    public void SetMother(int mother, Vector3 motherPos)
    {
        this.motherType = mother;
        //gm.togglechooseMenu();    //apresenta o menu de escolha

        //auto give next gen
        MakeNextGen();
        gm.NewGen(catOne,this);
        gm.setSavePoint(motherPos);
    }

    public void MakeNextGen()
    {
        catOne = father.Copy();
        catOne.RandomAllStatus(0.9f, 1.1f);
        catOne.RandomSingleStatus(1.1f,1.35f,motherType);
        
    }
}
