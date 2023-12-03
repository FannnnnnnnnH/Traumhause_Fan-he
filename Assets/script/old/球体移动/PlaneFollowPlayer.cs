using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFollowPlayer : MonoBehaviour
{
    public float rotationSpeed = 5f; // 球体旋转速度
    //public float moveSpeed = 5f; // 玩家移动速度
    public Camera playerCamera; // 球体对象的Transform组件
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
        // 计算玩家的移动方向

        //Vector3 movement = new Vector3( horizontalInput, mouseX, verticalInput).normalized;
        Vector3 movement = new Vector3(-verticalInput, mouseX, horizontalInput).normalized;
       
        Quaternion rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        // 将旋转角度应用于球体对象的Transform组件
        this.transform.Rotate(movement* rotationSpeed * Time.deltaTime, Space.World);

        
    }
}
