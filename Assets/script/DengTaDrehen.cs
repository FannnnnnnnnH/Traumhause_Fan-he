using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * DengTaDrehen.cs
 * 
 * �ýű����ƵƵ���ת��
 * ������
 * ������
 * ����: [FanHAHA]
 * ����: [����]
 */
public class DengTaDrehen : MonoBehaviour
{
    
    public float rotationSpeed = 6; // ��ת�ٶȣ�ÿ����ת�ĽǶ�
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
