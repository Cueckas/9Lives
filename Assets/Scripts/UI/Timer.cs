using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] NumberEventChannel timerEvent;
    [SerializeField] VoidEventChannel dieChannel;
    [SerializeField] VoidEventChannel youngEventChannel;
    [SerializeField] VoidEventChannel middleAgeEventChannel;
    [SerializeField] VoidEventChannel oldAgeEventChannel;

    //[SerializeField]
    public HealthBar lifeBar;

    float time =10;
    float totalTime;
    bool start = true;

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

            // Check if time is more than 75% of total time
            if (time > totalTime * 0.8f)
            {
                youngEventChannel?.Broadcast();
            }
            // Check if time is between 50% and 75% of total time
            else if (time > totalTime * 0.4f && time <= totalTime * 0.8f)
            {
                middleAgeEventChannel?.Broadcast();
            }
            // Check if time is less than 50% of total time
            else if (time <= totalTime * 0.4f)
            {
                oldAgeEventChannel?.Broadcast();
            }

            if (time <= 0)
            {
                start = false;
                Debug.Log("game end");
                dieChannel.Broadcast();
            }
        }
    }

    void setTime(float set)
    {
        totalTime = set;
        time = set;
        start = true;
        lifeBar.SetMaxHealth(set);
       
    }
}
