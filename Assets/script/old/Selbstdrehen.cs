using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selbstdrehen : MonoBehaviour
{
    public float rotationSpeed = 10f; // 自转速度
    public float rotationFactor = 0.1f; // 旋转系数

    private Quaternion targetRotation; // 目标旋转角度

    void Start()
    {
        targetRotation = transform.rotation; // 初始时与父物体保持一致
    }

    void Update()
    {
        // 获取父物体的朝向
        Quaternion parentRotation = transform.parent.rotation;

        // 计算子物体自身朝向的增量旋转
        Quaternion desiredRotation = Quaternion.Euler(0f, rotationSpeed * rotationFactor * Time.deltaTime, 0f);

        // 更新目标旋转角度
        targetRotation = targetRotation * desiredRotation;

        // 使用插值逐渐将子物体的旋转接近目标旋转
        transform.rotation = Quaternion.Lerp(transform.rotation, parentRotation * targetRotation, Time.deltaTime * 5f);
    }
}
