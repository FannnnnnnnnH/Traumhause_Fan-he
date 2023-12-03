using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * PlayerInteractionMove.cs
 * 
 * 该脚本控制玩家在场景中点击鼠标左键时，捕捉并移动特定物体。
 * 
 * 公共属性：
 * - objectPosition: 用于设置物体的目标位置的 Transform。
 * - objectsArray: 包含可能被移动的物体的数组。
 * - interactionDistance: 与物体交互的最大距离。
 * - grabDistance: 调整物体距离相机的距离。
 * 
 * 方法：
 * - Update: 检测玩家是否按下鼠标左键，发射射线并捕捉并移动特定物体。
 * - FindObjectInArray: 在物体数组中查找与射线命中的物体匹配的对象。
 * 
 * 作者: [FanHAHA]
 * 日期: [日期]
 */

public class PlayerInteractionMove : MonoBehaviour
{
    public Transform objectPosition;
    public GameObject[] objectsArray;
    public float interactionDistance=1;

    public float grabDistance = 0.2f; // 调整物体距离相机的距离


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            // 发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float raycastDistance = Camera.main.farClipPlane;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                GameObject foundObject = FindObjectInArray(hit.collider.gameObject);
                Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);
             
                if (foundObject != null)
                {
                    Vector3 mouseScreen = Input.mousePosition;
                    mouseScreen.z = grabDistance;

                    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
                    foundObject.transform.position = mouseWorld;


                }
            }
        }
    }
    GameObject FindObjectInArray(GameObject targetObject)
    {
        // 使用 LINQ 查找数组中是否存在匹配的物体
        GameObject foundObject = objectsArray.FirstOrDefault(obj => obj == targetObject);

        // 如果找到了匹配的物体，则返回它，否则返回 null
        return foundObject;
    }
   
}
