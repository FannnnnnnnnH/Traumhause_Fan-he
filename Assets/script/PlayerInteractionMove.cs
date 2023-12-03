using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * PlayerInteractionMove.cs
 * 
 * �ýű���������ڳ����е��������ʱ����׽���ƶ��ض����塣
 * 
 * �������ԣ�
 * - objectPosition: �������������Ŀ��λ�õ� Transform��
 * - objectsArray: �������ܱ��ƶ�����������顣
 * - interactionDistance: �����彻���������롣
 * - grabDistance: ���������������ľ��롣
 * 
 * ������
 * - Update: �������Ƿ������������������߲���׽���ƶ��ض����塣
 * - FindObjectInArray: �����������в������������е�����ƥ��Ķ���
 * 
 * ����: [FanHAHA]
 * ����: [����]
 */

public class PlayerInteractionMove : MonoBehaviour
{
    public Transform objectPosition;
    public GameObject[] objectsArray;
    public float interactionDistance=1;

    public float grabDistance = 0.2f; // ���������������ľ���


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            // ��������
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
        // ʹ�� LINQ �����������Ƿ����ƥ�������
        GameObject foundObject = objectsArray.FirstOrDefault(obj => obj == targetObject);

        // ����ҵ���ƥ������壬�򷵻��������򷵻� null
        return foundObject;
    }
   
}
