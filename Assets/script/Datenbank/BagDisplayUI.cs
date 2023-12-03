using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * BagDisplayUI.cs
 * 
 * �ýű�������ұ�����UI�е���ʾ����������UI��������Ʒ�Ĳ���͸��¡�
 * 
 * �������ԣ�
 * - mainItem: ���� MainItem �ű������汳�����ݡ�
 * - gridPrefab: ����������ʾ�������Ԥ�Ƽ���
 * - myBag: ���ñ��� UI GameObject��
 * 
 * ������
 * - Awake: �ڼ��ؽű�ʵ��ʱ���á�ȷ�� BagDisplayUI ֻ��һ��ʵ����
 * - OnEnable: �����ñ�����ʾʱ���á�����UI��
 * - insertItemToUI: ����Ʒ����UI������ʾ��ͼ���������
 * - updateItemToUI: ͨ���ڱ�������ʾ��������Ʒ����UI��
 * 
 * ���ߣ�[Fan He]
 * ���ڣ�[����]
 */
public class BagDisplayUI : MonoBehaviour
{
    static BagDisplayUI bagDisplayUI;

    public MainItem mainItem;
    public GridPrefab gridPrefab;
    public GameObject myBag;

    private void Awake()
    {
        if (bagDisplayUI != null)
        {
            Destroy(this.gameObject);
        }
        bagDisplayUI = this;
    }
    private void OnEnable()
    {
        updateItemToUI();
    }

    /// <summary>
    /// ��UI�н�һ����������ݲֿ���ʾ����
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item item)
    {

        GridPrefab grid = Instantiate(bagDisplayUI.gridPrefab, bagDisplayUI.myBag.transform);
        grid.gridImage.sprite = item.itemImage;
        grid.girdNum.text = item.itemNum.ToString();

    }

    /// <summary>
    /// ���������ݲֿ�������������ʾ��UI��
    /// </summary>
    public static void updateItemToUI()
    {
       
        int childCount = bagDisplayUI.myBag.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(bagDisplayUI.myBag.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < bagDisplayUI.mainItem.itemList.Count; i++)
        {
            insertItemToUI(bagDisplayUI.mainItem.itemList[i]);
        }

        /*for (int i = 0; i < bagDisplayUI.myBag.transform.childCount; i++)
       {
           Destroy(bagDisplayUI.myBag.transform.GetChild(i).gameObject);
       }
       for (int i = 0; i < bagDisplayUI.mainItem.itemList.Count; i++)
       {
           insertItemToUI(bagDisplayUI.mainItem.itemList[i]);
       }*/
    }


}
