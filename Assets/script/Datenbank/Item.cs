using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Item.cs
 * 
 * 该脚本继承自 ScriptableObject，用于创建可在 Unity 编辑器中创建的物体数据资源。
 * 每个物体包含物体名、在 UI 中显示的图片、持有物体的数量、以及物体信息的描述。
 * 
 * CreateAssetMenu 属性用于在 Unity 编辑器中创建新的物体数据资源。
 * 
 * 公共属性：
 * - itemName: 物体的名称。
 * - itemImage: 在 UI 中显示的物体图片。
 * - itemNum: 持有该物体的数量。
 * - itemInfo: 物体信息的描述，通过 TextArea 改变输入框格式。
 * 
 * 作者：[你的名字]
 * 日期：[日期]
 */
[CreateAssetMenu(fileName = "New Item", menuName = "Bag/New Item")]
public class Item : ScriptableObject
{
    //物体名、需要在UI中显示的图片、持有物体的数量、物体信息的描述
    public string itemName;
    public Sprite itemImage;
    public int itemNum;
    [TextArea] //改变输入框格式，提示输入框容量
    public string itemInfo; 

}
