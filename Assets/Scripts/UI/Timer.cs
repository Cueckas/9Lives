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

    public HealthBar lifeBar;

    float time = 10;
    float totalTime;
    bool start = true;
    bool triggeredYoung = false, triggeredMiddle = false, triggeredOld = false;

    void OnEnable()
    {
        timerEvent.AddListener(setTime);
    }

    void Start()
    {
        
        time = totalTime;
    }

    void FixedUpdate()
    {
        if (start)
        {
            time -= Time.fixedDeltaTime;
            lifeBar.SetHealth(time);
            timeText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

            if (time > totalTime * 0.8f && !triggeredYoung)
            {
                youngEventChannel?.Broadcast();
                triggeredYoung = true;
            }
            else if (time > totalTime * 0.4f && time <= totalTime * 0.8f && !triggeredMiddle)
            {
                middleAgeEventChannel?.Broadcast();
                triggeredMiddle = true;
            }
            else if (time <= totalTime * 0.4f && !triggeredOld)
            {
                oldAgeEventChannel?.Broadcast();
                triggeredOld = true;
            }

            if (time <= 0)
            {
                start = false;
                Debug.Log("Game End");
                dieChannel.Broadcast();
            }
        }
    }

    void setTime(float set)
    {
        totalTime = set;
        time = set;
        start = true;
        triggeredYoung = triggeredMiddle = triggeredOld = false;
        lifeBar.SetMaxHealth(set);
    }
}