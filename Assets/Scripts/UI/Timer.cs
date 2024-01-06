using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timeText;

    [SerializeField] NumberEventChannel timerEvent;

    [SerializeField] VoidEventChannel dieChannel;

    //[SerializeField]
    public HealthBar lifeBar;

    float time;

    bool start = false;

    void OnEnable()
    {
        timerEvent.AddListener(setTime);
    }

    void FixedUpdate()
    {
        if (start)
        {
            time -= Time.fixedDeltaTime;

            lifeBar.SetHealth(time);
            timeText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
            if (time <= 0)
            {
                start = false;
                Debug.Log("game end");
                dieChannel.Broadcast();
                // end game or change cat
            }
        }
        
    }

    void setTime(float set)
    {
        time = set;
        start = true;
        lifeBar.SetMaxHealth(set);
       
    }
}
