using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selbstdrehen : MonoBehaviour
{
    public float rotationSpeed = 10f; // ��ת�ٶ�
    public float rotationFactor = 0.1f; // ��תϵ��

    private Quaternion targetRotation; // Ŀ����ת�Ƕ�

    void Start()
    {
        targetRotation = transform.rotation; // ��ʼʱ�븸���屣��һ��
    }

    void Update()
    {
        // ��ȡ������ĳ���
        Quaternion parentRotation = transform.parent.rotation;

        // �����������������������ת
        Quaternion desiredRotation = Quaternion.Euler(0f, rotationSpeed * rotationFactor * Time.deltaTime, 0f);

        // ����Ŀ����ת�Ƕ�
        targetRotation = targetRotation * desiredRotation;

        // ʹ�ò�ֵ�𽥽����������ת�ӽ�Ŀ����ת
        transform.rotation = Quaternion.Lerp(transform.rotation, parentRotation * targetRotation, Time.deltaTime * 5f);
    }
}
