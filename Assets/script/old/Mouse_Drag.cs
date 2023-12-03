using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Drag : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mCamera;
    ////景深
    public float depth = 0.5f;


    /// 鼠标接触物体

    void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }
    /// <summary>
    /// 鼠标离开物体
    /// </summary>
    void OnMouseExit()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    /// <summary>
    /// 鼠标停在物体上
    /// </summary>
    void OnMouseOver()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.transform.Rotate(Vector3.up, 45 * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// 鼠标拖拽物体
    /// </summary>
    void OnMouseDrag()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        //MoveObject_fixdepth();
    }
    /// <summary>
    /// 鼠标拖拽物体的实现逻辑
    /// </summary>
    void MoveObject_fixdepth()
    {
        Vector3 mouseWorld = mCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));

        // 设置物体的位置
        transform.position = new Vector3(mouseWorld.x, mouseWorld.y, depth);


    }

}
