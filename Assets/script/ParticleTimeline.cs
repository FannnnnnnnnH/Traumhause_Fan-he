using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ParticleTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Player playerMoveSpeed;
    protected bool isTriggered;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider player)
    { 
        TimeController.DayTime nowTime = TimeController.currentDayTime;
        //如果现在是白天的话就返回,如果是晚上就可以播放
        if (nowTime==TimeController.DayTime.Day)
        {
            return;
        }

        director.Play();
        isTriggered = true;
       // playerMoveSpeed.enabled = false;

    }
}
