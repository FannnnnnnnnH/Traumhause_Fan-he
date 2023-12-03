using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MainItem.cs
 * 
 * �ýű��̳��� ScriptableObject�����ڴ������� Unity �༭���д�������Ҫ����������Դ��
 * ��Ҫ�������ݰ���һ�������б����ڴ洢��ҵ������档
 * 
 * CreateAssetMenu ���������� Unity �༭���д����µ���Ҫ����������Դ��
 * 
 * �������ԣ�
 * - itemList: �洢���������������б�
 * 
 * ���ߣ�[�������]
 * ���ڣ�[����]
 */
[CreateAssetMenu(fileName = "New MainItem", menuName = "Bag/New MainItem")]
public class MainItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
