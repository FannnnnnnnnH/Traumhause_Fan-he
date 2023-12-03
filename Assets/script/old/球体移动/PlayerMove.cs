using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Transform playerTransform;
    private Transform sphereTransform;
    public int obstacleDetectionDistance=1;

    public float mouseSensitivity = 3f;
    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;

    public Transform planet; // ������������
    //public float movementSpeed = 5f; // ����ƶ��ٶ�

    private void Update()
    {
        // ��ȡ��ҵ�����
         horizontalInput = Input.GetAxis("Horizontal");
         verticalInput = Input.GetAxis("Vertical");
        RaycastHit hit;
        Vector3 point_dir = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, point_dir, out hit, 1000f, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, point_dir * 10, Color.red);
            Quaternion nextRot = Quaternion.LookRotation(Vector3.Cross(hit.normal, Vector3.Cross(transform.forward, hit.normal)), hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f);
        }
        if (verticalInput!=0)
        {
            Vector3 movement = new Vector3(0f, point_dir.y, verticalInput).normalized*Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
        


    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + transform.up * 0.1f; // ���Ը�����ҵ�λ�ý������߼��

        if (Physics.Raycast(raycastOrigin, -transform.up, out hit, obstacleDetectionDistance))
        {
            // �����⵽�ϰ������Ϊ��Ҳ��ڵ�����
            return false;
        }

        return true;
    }
    

    //private bool CheckObstacle(Vector3 movement)
    //{
    //    RaycastHit hit;
    //    Vector3 raycastOrigin = transform.position;

    //    if (Physics.Raycast(raycastOrigin, movement, out hit, obstacleDetectionDistance))
    //    {
    //        // �����⵽�ϰ���򷵻� true����ʾ�������ϰ���
    //        return true;
    //    }

    //    return false;
    //}
}
