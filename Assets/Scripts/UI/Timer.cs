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

    public GameObject allghosts;

    public HealthBar timeBar;

    float time;
    float totalTime;
    bool start = false;
    bool triggeredYoung = false, triggeredMiddle = false, triggeredOld = false;

    void OnEnable()
    {
        timerEvent.AddListener(setTime);
    }

    void FixedUpdate()
    {
        if (start)
        {
            time -= Time.fixedDeltaTime;
            timeBar.SetHealth(time);
            timeText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

            if (!triggeredYoung && time > totalTime * 0.8f )
            {
                youngEventChannel?.Broadcast();
                triggeredYoung = true;

                allghosts.SetActive(false);
            }
            else if (!triggeredMiddle && time > totalTime * 0.4f && time <= totalTime * 0.8f)
            {
                middleAgeEventChannel?.Broadcast();
                triggeredMiddle = true;
                allghosts.SetActive(false);
            }
            else if (!triggeredOld && time <= totalTime * 0.4f )
            {
                oldAgeEventChannel?.Broadcast();
                triggeredOld = true;
                allghosts.SetActive(true);

            }

            if (time <= 0)
            {
                start = false;
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
        timeBar.SetMaxHealth(set);
    }
}