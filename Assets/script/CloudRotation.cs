using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * CloudRotation.cs
 * 
 * �ýű������Ƶ���׬��
 * -��ת�ٶ�
 * -��һ�θı���ת��ʱ��
 * ������
 * ����: [FanHAHA]
 * ����: [����]
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

        // ��Update��ʹ���尴�������תֵ��ת
        transform.Rotate(randomRotation * rotationSpeed * Time.deltaTime);
    }
    void SetRandomRotationAndTime()
    {
        // ���������תֵ
        randomRotation = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f)
        );

        // ��׼����ʹ���Ϊ��λ������Ȼ������ٶ�
        randomRotation.Normalize();

        // ������һ�θ��ĵ�ʱ��
        nextChangeTime = Time.time + Random.Range(15.0f, 30.0f);
    }
}
