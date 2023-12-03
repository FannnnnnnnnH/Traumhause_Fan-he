using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float rotationSpeed = 0.06f; // 自转速度，调整此值来控制旋转速度
    private float elapsedTime = 0f;

    private void FixedUpdate()
    {
        // 自转角度增量，根据时间流逝和自转速度计算
        float rotationIncrement = Time.deltaTime * 360f / 600f * rotationSpeed;

        // 更新自转角度
        transform.Rotate(Vector3.up, rotationIncrement);

        // 更新游戏时间
        elapsedTime += Time.deltaTime;

        // 判断是否到达一天的结束，即10分钟
        if (elapsedTime >= 600f)
        {
            elapsedTime = 0f; // 重置时间
            //dayCounter++; // 增加天数计数，如果需要的话
        }
    }
}
