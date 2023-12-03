using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DengTaDrehen.cs
 * 
 * 该脚本控制灯的自转。
 * 参数：
 * 方法：
 * 作者: [FanHAHA]
 * 日期: [日期]
 */
public class DengTaDrehen : MonoBehaviour
{
    
    public float rotationSpeed = 6; // 自转速度，每秒旋转的角度
    private Quaternion worldRotation;
    void Start()
    {
        //worldRotation = parentObject.rotation * Quaternion.Euler(rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }
}
