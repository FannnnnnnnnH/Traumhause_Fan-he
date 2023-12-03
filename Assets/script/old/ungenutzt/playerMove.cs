using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMove : MonoBehaviour
{

    private float moveSpeed;//摄像机的移动速度
    public GameObject Eye;
    void Start()
    {
        moveSpeed = 2;
    }

    Vector3 rot = new Vector3(0, 0, 0);

    void Update()
    {
        //鼠键控制移动
        WASD();

        if (Input.anyKey)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    /// <summary>
    /// 鼠键控制player移动
    /// </summary>
    void WASD()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                //Debug.Log(Input.GetAxis("Mouse X"));
                if (Input.GetAxis("Mouse X") < 0.1f && Input.GetAxis("Mouse X") > -0.1f)
                {
                    // return;
                }
                this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.fixedDeltaTime * 200, 0));//摄像机的旋转速度
                //clearArrow(false);
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {
                if (Input.GetAxis("Mouse Y") < 0.1f && Input.GetAxis("Mouse Y") > -0.1f)
                {
                    Debug.Log("返回");
                    //  return;
                }
                Eye.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * -200, 0, 0));//摄像机的旋转速度
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-Vector3.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }
}
