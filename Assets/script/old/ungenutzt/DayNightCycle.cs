using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float rotationSpeed = 0.06f; // ��ת�ٶȣ�������ֵ��������ת�ٶ�
    private float elapsedTime = 0f;

    private void FixedUpdate()
    {
        // ��ת�Ƕ�����������ʱ�����ź���ת�ٶȼ���
        float rotationIncrement = Time.deltaTime * 360f / 600f * rotationSpeed;

        // ������ת�Ƕ�
        transform.Rotate(Vector3.up, rotationIncrement);

        // ������Ϸʱ��
        elapsedTime += Time.deltaTime;

        // �ж��Ƿ񵽴�һ��Ľ�������10����
        if (elapsedTime >= 600f)
        {
            elapsedTime = 0f; // ����ʱ��
            //dayCounter++; // �������������������Ҫ�Ļ�
        }
    }
}
