using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*
 * PlayerInteractionController.cs
 * 
 * 该脚本控制玩家的交互行为，包括触发光源、旋转门窗和切换视频播放。
 * 
 * 公共属性：
 * - interactionDistance: 交互距离，玩家需要在该距离内与物体进行交互。
 * - interactionKey: 触发交互的键。
 * - interactables: 一组可交互对象及其类型定义。
 * 
 * 方法：
 * - Update: 检测玩家是否按下交互键，射线检测交互物体，并执行相应的交互行为。
 * - TryInteract: 与 Update 方法类似，用于在其他情况下尝试交互。
 * - PerformInteraction: 执行与特定交互类型相关的行为。
 * - ToggleVideo: 切换视频播放状态（播放/暂停）。
 * - ToggleLights: 切换光源状态（开启/关闭）。
 * - RotateDoor: 旋转门的逻辑，根据当前角度判断是打开还是关闭。
 * - RotateWindow: 旋转窗户的逻辑，根据当前角度判断是打开还是关闭。
 * - GetInteractable: 根据射线检测的物体获取相应的可交互对象。
 * 
 * 作者: [FanHAHA]
 * 日期: []
 */

public class PlayerInteractionController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public KeyCode interactionKey = KeyCode.Space;

    public enum InteractableType { Light, Door, Window, Video }

    [System.Serializable]
    public class Interactable
    {
        public InteractableType type;
        public GameObject[] objects;
    }

    public Interactable[] interactables;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Debug.Log("有");
            //Ray ray = new Ray(transform.position, transform.forward);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);
                Interactable interactable = GetInteractable(hit.collider.gameObject);
                if (interactable != null)
                {
                    
                    PerformInteraction(interactable, hit.collider.gameObject);
                }
            }
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);
            Interactable interactable = GetInteractable(hit.collider.gameObject);
            if (interactable != null)
            {
               
                PerformInteraction(interactable, hit.collider.gameObject);
            }
        }
    }

    void PerformInteraction(Interactable interactable, GameObject hitObject)
    {
        switch (interactable.type)
        {
            case InteractableType.Light:
                ToggleLights(hitObject);
                break;

            case InteractableType.Door:
                RotateDoor(hitObject);
                break;

            case InteractableType.Window:
                RotateWindow(hitObject);
                break;
            case InteractableType.Video:
                ToggleVideo(hitObject);
                break;
        }

       
    }

    private void ToggleVideo(GameObject Videos)
    {
       
            VideoPlayer player = Videos.GetComponent<VideoPlayer>();
            if (player.isPlaying)
            {
                Debug.Log("有吗");
                player.GetComponent<VideoPlayer>().Pause();
            }
            else
            {
                Debug.Log("有吗");
                player.GetComponent<VideoPlayer>().Play();
            }
        
    }

    void ToggleLights(GameObject light)
    {
            Transform childTransform = light.transform.Find("Spot Light");
            Light spotLight = childTransform.GetComponent<Light>();

            if (spotLight != null)
            {
                // 切换灯光的状态（开启/关闭）
                spotLight.enabled = !spotLight.enabled;
            }
            else
            {
                Debug.LogError("Child object does not have a Light component.");
            }
        
    }

    void RotateDoor(GameObject door)
    {
         // 在这里实现门的旋转逻辑
            if(door.transform.rotation.eulerAngles.y >= 80f)
            {
                door.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            }
            else door.transform.localRotation = Quaternion.Euler(0f, 90f, 0f); 
        
    }

    void RotateWindow(GameObject window)
    {
       
            if (window.transform.rotation.eulerAngles.y >= 80f)
            {
                window.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            }
            else window.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        
    }
    //接受一个类型为 GameObject 的参数 hitObject，并且返回一个类型为 Interactable 的对象。
    Interactable GetInteractable(GameObject hitObject)
    {
        //它遍历一个数组或集合（在这里是 interactables），并且对于数组中的每一个元素，将其赋值给变量 interactable。
        foreach (Interactable interactable in interactables)
        {
            //检查 interactable.objects 数组中是否存在一个与 hitObject 相等的对象。
            //interactable.objects 是 Interactable 类的一个属性
            //System.Array.Exists 是一个用于在数组中查找元素是否存在的方法。
            //Lambda 表达式 obj => obj == hitObject 表示对于数组中的每个元素 obj，检查是否与 hitObject 相等。
            if (System.Array.Exists(interactable.objects, obj => obj == hitObject))
            {
                return interactable;
            }
        }
        return null;
    }
}
