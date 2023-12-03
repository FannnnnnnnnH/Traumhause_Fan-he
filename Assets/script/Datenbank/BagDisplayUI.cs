using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * BagDisplayUI.cs
 * 
 * 该脚本管理玩家背包在UI中的显示。它处理了UI网格中物品的插入和更新。
 * 
 * 公共属性：
 * - mainItem: 引用 MainItem 脚本，保存背包数据。
 * - gridPrefab: 引用用于显示网格项的预制件。
 * - myBag: 引用背包 UI GameObject。
 * 
 * 方法：
 * - Awake: 在加载脚本实例时调用。确保 BagDisplayUI 只有一个实例。
 * - OnEnable: 在启用背包显示时调用。更新UI。
 * - insertItemToUI: 将物品插入UI网格，显示其图像和数量。
 * - updateItemToUI: 通过在背包中显示的所有物品更新UI。
 * 
 * 作者：[Fan He]
 * 日期：[日期]
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
    /// 在UI中将一个物体的数据仓库显示出来
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item item)
    {

        GridPrefab grid = Instantiate(bagDisplayUI.gridPrefab, bagDisplayUI.myBag.transform);
        grid.gridImage.sprite = item.itemImage;
        grid.girdNum.text = item.itemNum.ToString();

    }

    /// <summary>
    /// 将背包数据仓库中所有物体显示在UI上
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
