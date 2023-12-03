using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CloudRotation.cs
 * 
 * 该脚本控制云的自赚。
 * -自转速度
 * -下一次改变自转的时间
 * 方法：
 * 作者: [FanHAHA]
 * 日期: [日期]
 */
public class CloudRotation : MonoBehaviour
{
    private Vector3 randomRotation;
    private float nextChangeTime;
    public float rotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChangeTime)
        {
            SetRandomRotationAndTime();
        }

        // 在Update中使物体按照随机旋转值旋转
        transform.Rotate(randomRotation * rotationSpeed * Time.deltaTime);
    }
    void SetRandomRotationAndTime()
    {
        // 设置随机旋转值
        randomRotation = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f)
        );

        // 标准化，使其成为单位向量，然后乘以速度
        randomRotation.Normalize();

        // 设置下一次更改的时间
        nextChangeTime = Time.time + Random.Range(15.0f, 30.0f);
    }
}
