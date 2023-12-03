using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ScenesItem.cs
 * 
 * 该脚本附加在场景物体上，用于处理玩家与物体的碰撞触发。在玩家与物体碰撞时，将物体的数据添加到背包中，
 * 更新背包显示，并销毁物体。
 * 
 * 公共属性：
 * - item: 物体的数据仓库，保存物体的信息。
 * - mainItem: 背包的数据仓库，保存背包的信息。
 * 
 * 方法：
 * - OnTriggerEnter: 当物体与玩家发生碰撞时调用。如果物体不在背包中，则添加到背包中，增加物品数量，更新UI，并销毁物体。
 * 
 * 作者：[你的名字]
 * 日期：[日期]
 */
public class ScenesItem : MonoBehaviour
{
    //物体的数据仓库
    public Item item;
    //背包的数据仓库
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
