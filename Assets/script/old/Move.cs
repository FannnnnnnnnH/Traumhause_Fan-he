using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    bool isOk = false;  //初始值false
    private RaycastHit hit;
    public Camera ca;
    public Object player;
    private Ray ra;
    private int flag = 0;

    void Start()
    {
        //获取MeshRenderer的材质球并变为红色
        //this.GetComponent<MeshRenderer>().material.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        //判断是否按下左键
        if (Input.GetMouseButtonDown(0))
        {
            //发射线   
            ra = ca.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit))
            {
                //我要移动的物体名字叫Sphere
                if (hit.transform.name == "圆柱_1_22")
                {
                    // hit.collider.gameObject.transform.position = player

                    if (flag == 0)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                    //print("TEXT");
                }
            }
        }
        if (flag == 1)
        {
            hit.collider.gameObject.transform.position = ca.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.collider.gameObject.transform.position.z));
        }
    }

}
