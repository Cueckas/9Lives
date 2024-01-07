using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{

    [SerializeField] float timeLife;

    [SerializeField] float hp;

    [SerializeField] float speed;

    [SerializeField] float jumpForce;

    [SerializeField] float attack;

    [SerializeField] float attackSpeed;

    [SerializeField] float attackRate;

    [SerializeField] NumberEventChannel timerEvent;

    [SerializeField] VoidEventChannel dieChannel;

    [SerializeField] PositionEventChannel positionChannel;

    [SerializeField] VoidEventChannel youngEventChannel;

    [SerializeField] VoidEventChannel middleAgeEventChannel;

    [SerializeField] VoidEventChannel oldAgeEventChannel;

    // Start is called before the first frame update


    void Start()
    {
        timerEvent.Broadcast(timeLife);
        dieChannel.AddListener(Die);
    }

    void Die(){
        dieChannel.RemoveListener(Die);
        positionChannel.Broadcast(transform.GetChild(2).position);
        Destroy(gameObject);
    }
}
