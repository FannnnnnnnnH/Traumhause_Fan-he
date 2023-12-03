using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFollowPlayer : MonoBehaviour
{
    public float rotationSpeed = 5f; // ������ת�ٶ�
    //public float moveSpeed = 5f; // ����ƶ��ٶ�
    public Camera playerCamera; // ��������Transform���
    public float mouseSensitivity;


    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    //private Rigidbody rb;

    private void Start()
    {
        //  rb = GetComponent<Rigidbody>();
       

    }

     void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Playerjump.isObjectCollision)
        {
            if (verticalInput>=0)
            {
                verticalInput = 0;
            }
            
        }
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        // ������ҵ��ƶ�����

        //Vector3 movement = new Vector3( horizontalInput, mouseX, verticalInput).normalized;
        Vector3 movement = new Vector3(-verticalInput, mouseX, horizontalInput).normalized;
       
        Quaternion rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        // ����ת�Ƕ�Ӧ������������Transform���
        this.transform.Rotate(movement* rotationSpeed * Time.deltaTime, Space.World);

        
    }
}
