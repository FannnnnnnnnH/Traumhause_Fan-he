using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*
 * PlayerInteractionController.cs
 * 
 * �ýű�������ҵĽ�����Ϊ������������Դ����ת�Ŵ����л���Ƶ���š�
 * 
 * �������ԣ�
 * - interactionDistance: �������룬�����Ҫ�ڸþ�������������н�����
 * - interactionKey: ���������ļ���
 * - interactables: һ��ɽ������������Ͷ��塣
 * 
 * ������
 * - Update: �������Ƿ��½����������߼�⽻�����壬��ִ����Ӧ�Ľ�����Ϊ��
 * - TryInteract: �� Update �������ƣ���������������³��Խ�����
 * - PerformInteraction: ִ�����ض�����������ص���Ϊ��
 * - ToggleVideo: �л���Ƶ����״̬������/��ͣ����
 * - ToggleLights: �л���Դ״̬������/�رգ���
 * - RotateDoor: ��ת�ŵ��߼������ݵ�ǰ�Ƕ��ж��Ǵ򿪻��ǹرա�
 * - RotateWindow: ��ת�������߼������ݵ�ǰ�Ƕ��ж��Ǵ򿪻��ǹرա�
 * - GetInteractable: �������߼��������ȡ��Ӧ�Ŀɽ�������
 * 
 * ����: [FanHAHA]
 * ����: []
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
            Debug.Log("��");
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
                Debug.Log("����");
                player.GetComponent<VideoPlayer>().Pause();
            }
            else
            {
                Debug.Log("����");
                player.GetComponent<VideoPlayer>().Play();
            }
        
    }

    void ToggleLights(GameObject light)
    {
            Transform childTransform = light.transform.Find("Spot Light");
            Light spotLight = childTransform.GetComponent<Light>();

            if (spotLight != null)
            {
                // �л��ƹ��״̬������/�رգ�
                spotLight.enabled = !spotLight.enabled;
            }
            else
            {
                Debug.LogError("Child object does not have a Light component.");
            }
        
    }

    void RotateDoor(GameObject door)
    {
         // ������ʵ���ŵ���ת�߼�
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
    //����һ������Ϊ GameObject �Ĳ��� hitObject�����ҷ���һ������Ϊ Interactable �Ķ���
    Interactable GetInteractable(GameObject hitObject)
    {
        //������һ������򼯺ϣ��������� interactables�������Ҷ��������е�ÿһ��Ԫ�أ����丳ֵ������ interactable��
        foreach (Interactable interactable in interactables)
        {
            //��� interactable.objects �������Ƿ����һ���� hitObject ��ȵĶ���
            //interactable.objects �� Interactable ���һ������
            //System.Array.Exists ��һ�������������в���Ԫ���Ƿ���ڵķ�����
            //Lambda ���ʽ obj => obj == hitObject ��ʾ���������е�ÿ��Ԫ�� obj������Ƿ��� hitObject ��ȡ�
            if (System.Array.Exists(interactable.objects, obj => obj == hitObject))
            {
                return interactable;
            }
        }
        return null;
    }
}
