using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWindow : MonoBehaviour
{
    public string stage;
    public string tag;
    GameObject gameobject;
    public bool WindowSwitch = false;
    private Rect WindowRect = new Rect(20, 20, 240, 80);
    private void OnGUI()
    {

        if (WindowSwitch)
        {
            gameobject = GameObject.FindWithTag(tag);
            if (gameobject.GetComponent<MeshRenderer>() != null)
            {
                Material material = gameobject.GetComponent<MeshRenderer>().material;
                string name = material.name;
                string[] str = name.Split('_');
                stage = str[1];
                string[] str1 = stage.Split('(');
                stage = str1[0];
            }
            else if (gameobject.GetComponentInChildren<MeshRenderer>() != null)
            {
                Material material = gameobject.GetComponentInChildren<MeshRenderer>().material;
                string name = material.name;
                string[] str = name.Split('_');
                stage = str[1];
                string[] str1 = stage.Split('(');
                stage = str1[0];
            }
            GUI.Window(0, WindowRect, DoMyWindow, "状态显示");
            //GUI.DragWindow(new Rect(0, 0, 2000, 2000));
            GUI.Label(new Rect(22, 40, 240, 80), "设备:" + tag);
            GUI.Label(new Rect(22, 60, 100, 100), "状态:" + stage);

        }
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(220, 0, 20, 20), "X"))

        {
            WindowSwitch = false;
        }

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { //首先判断是否点击了鼠标左键


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //定义一条射线，这条射线从摄像机屏幕射向鼠标所在位置
            RaycastHit hit; //声明一个碰撞的点(暂且理解为碰撞的交点)
            if (Physics.Raycast(ray, out hit)) //如果真的发生了碰撞，ray这条射线在hit点与别的物体碰撞了
            {
                tag = hit.collider.gameObject.tag;

                WindowSwitch = true;
                //GetComponent<Transform>().pos
            }

        }
    }
}
