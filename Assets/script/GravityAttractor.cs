using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * GravityAttractor.cs
 * 
 * �ýű����������������ܣ�ʹָ�������峯���ܵ������������ű������ӵ���Ϸ����
 * 
 * �������ԣ�
 * - gravity: ������ǿ�ȣ��������屻�����ĳ̶ȡ�
 * 
 * ������
 * - Attract(Transform body): �������峯��ʩ��������
 * ����: [Sebastian Lague] �ο� https://www.youtube.com/watch?v=TicipSVT-T8&t=1508s
 * �����޸Ĺ�:[FanHAHA]
 * ����: [����]
 */
public class GravityAttractor : MonoBehaviour
{
    public float gravity = -20f;
    Rigidbody rb;
    public void Attract(Transform body)
    {
        Vector3 targetPlayer = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        body.rotation = Quaternion.FromToRotation(bodyUp, targetPlayer)*body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetPlayer * gravity);
    }
    // Start is called before the first frame update
   
}
