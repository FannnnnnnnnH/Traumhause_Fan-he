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
        //��������ǰ���Ļ��ͷ���,��������ϾͿ��Բ���
        if (nowTime==TimeController.DayTime.Day)
        {
            return;
        }

        director.Play();
        isTriggered = true;
       // playerMoveSpeed.enabled = false;

    }
}
