using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MainItem.cs
 * 
 * 该脚本继承自 ScriptableObject，用于创建可在 Unity 编辑器中创建的主要物体数据资源。
 * 主要物体数据包含一个物体列表，用于存储玩家的物体库存。
 * 
 * CreateAssetMenu 属性用于在 Unity 编辑器中创建新的主要物体数据资源。
 * 
 * 公共属性：
 * - itemList: 存储玩家物体库存的物体列表。
 * 
 * 作者：[你的名字]
 * 日期：[日期]
 */
[CreateAssetMenu(fileName = "New MainItem", menuName = "Bag/New MainItem")]
public class MainItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
