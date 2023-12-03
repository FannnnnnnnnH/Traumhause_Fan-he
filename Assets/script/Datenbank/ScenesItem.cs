using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ScenesItem.cs
 * 
 * �ýű������ڳ��������ϣ����ڴ���������������ײ�������������������ײʱ���������������ӵ������У�
 * ���±�����ʾ�����������塣
 * 
 * �������ԣ�
 * - item: ��������ݲֿ⣬�����������Ϣ��
 * - mainItem: ���������ݲֿ⣬���汳������Ϣ��
 * 
 * ������
 * - OnTriggerEnter: ����������ҷ�����ײʱ���á�������岻�ڱ����У�����ӵ������У�������Ʒ����������UI�����������塣
 * 
 * ���ߣ�[�������]
 * ���ڣ�[����]
 */
public class ScenesItem : MonoBehaviour
{
    //��������ݲֿ�
    public Item item;
    //���������ݲֿ�
    public MainItem mainItem;

    private void Start()
    {
        this.GetComponent<MeshRenderer>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            if (!mainItem.itemList.Contains(item))
            {
                mainItem.itemList.Add(item);
            }
            item.itemNum += 1;
            BagDisplayUI.updateItemToUI();
           // this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
