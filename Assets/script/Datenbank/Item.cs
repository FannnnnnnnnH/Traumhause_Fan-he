using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Item.cs
 * 
 * �ýű��̳��� ScriptableObject�����ڴ������� Unity �༭���д���������������Դ��
 * ÿ������������������� UI ����ʾ��ͼƬ������������������Լ�������Ϣ��������
 * 
 * CreateAssetMenu ���������� Unity �༭���д����µ�����������Դ��
 * 
 * �������ԣ�
 * - itemName: ��������ơ�
 * - itemImage: �� UI ����ʾ������ͼƬ��
 * - itemNum: ���и������������
 * - itemInfo: ������Ϣ��������ͨ�� TextArea �ı�������ʽ��
 * 
 * ���ߣ�[�������]
 * ���ڣ�[����]
 */
[CreateAssetMenu(fileName = "New Item", menuName = "Bag/New Item")]
public class Item : ScriptableObject
{
    //����������Ҫ��UI����ʾ��ͼƬ�����������������������Ϣ������
    public string itemName;
    public Sprite itemImage;
    public int itemNum;
    [TextArea] //�ı�������ʽ����ʾ���������
    public string itemInfo; 

}
