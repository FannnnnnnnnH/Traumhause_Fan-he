using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * BlockManager.cs
 * 
 * 该脚本管理游戏中方块数据的序列化、保存和加载。
 * 包括用于序列化方块数据的类、用于存储方块实例的列表，以及使用 JSON 保存和加载方块的方法。
 * 
 * 公共属性：
 * - list: 存储表示方块位置和旋转的 BlockData 实例的列表。
 * - squarePrefab: 方块 GameObject 的预制体变量。
 * 
 * 方法：
 * - Start: 在第一帧更新之前调用。当前未用于加载花朵。
 * - SaveBlocks: 通过将其转换为 JSON 并写入文件来保存方块数据。
 * - LoadBlocks: 从 JSON 文件加载保存的方块数据并实例化方块 GameObject。
 * 
 * 类：
 * - BlockData: 表示方块位置和旋转的类。用于序列化。
 * - BlockDataList: 用于序列化的 BlockData 实例列表的持有类。
 * 
 * 作者：[你的名字]
 * 日期：[日期]
 */

//串行化,串行化是指存储和获取磁盘文件、内存或其他地方中的对象。
[System.Serializable]
public class BlockData
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isCollected;
    public float lastCollectedTime;
    // 其他方块属性...

    public BlockData(Vector3 position, Quaternion rotation, bool isCollected, float lastCollectedTime)
    {
        this.position = position;
        this.rotation = rotation;
        this.isCollected = isCollected;
        this.lastCollectedTime = lastCollectedTime;
    }
    public override string ToString()
    {
        return string.Format("Block Position: {0}, Block Rotation: {1}", position, rotation,isCollected,lastCollectedTime);
    }
}
[System.Serializable]
public class BlockDataList
{
    public List<BlockData> blockDataList = new List<BlockData>();
}
public class BlockManager : MonoBehaviour
{
    //这里要注意写法 和public List<BlockData> blockDataList = new List<BlockData>();其实是一个东西
    public List<BlockData> list = new List<BlockData>();
    public GameObject squarePrefab; // 添加方块预制体变量
    public float regrowIntervalSeconds = 30f;

    private Dictionary<Vector3, GameObject> blockObjects = new Dictionary<Vector3, GameObject>();

    //public static List<BlockManager> allBlockManagers = new List<BlockManager>();

    private void Awake()
    {
        // 在 Awake 中添加自身到 allBlockManagers 列表
        //allBlockManagers.Add(this);
    }
    private void Start()
    {
        //加载花朵
        LoadBlocks();
    }


        /// <summary>
        /// 好像只能用一次
        /// </summary>
        public void SaveBlocks()
    {
        BlockDataList blockDataList = new BlockDataList();
        blockDataList.blockDataList.AddRange(list); ;


        // 将方块数据转换为可保存的格式，例如 JSON
        string json = JsonUtility.ToJson(blockDataList);

        // 将数据写入到文件中
        string filePath = Application.persistentDataPath + "/blockData.json";
        File.WriteAllText(filePath, json);

        Debug.Log("Blocks saved successfully.");

       
        Debug.Log("加载成功");
        //blockDataList =new blo
        //File.WriteAllText(filePath, json);
        Debug.Log("--------------------------------- ");
    }

    public void LoadBlocks()
    {
        // 从文件中读取保存的数据
        string filePath = Application.persistentDataPath + "/blockData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // 将读取的数据转换回方块数据列表
            BlockDataList loadedData = JsonUtility.FromJson<BlockDataList>(json);
            list.AddRange(loadedData.blockDataList);
            for (int i = 0; i < loadedData.blockDataList.Count; i++)
            {
                GameObject square = Instantiate(squarePrefab, loadedData.blockDataList[i].position, loadedData.blockDataList[i].rotation);
                blockObjects.Add(loadedData.blockDataList[i].position, square);
            
            //list.Add(square);
            if (!loadedData.blockDataList[i].isCollected)
                {
                    square.SetActive(false);
                    StartCoroutine(EnableObjectAfterDelay(loadedData.blockDataList[i],square, 15f));
                }
            }
            
            Debug.Log("Blocks loaded successfully.");
        }
        else
        {
            Debug.Log("blockData.json file not found.");
        }
    }
    public void HandleBlockInteraction(Vector3 hitPosition)
    {
        // 检查是否射中了方块
        BlockData hitBlock = list.Find(block => block.position == hitPosition);

        if (hitBlock != null)
        {
            // 修改 isCollected 属性
            hitBlock.isCollected = false;
            //hitBlock.lastCollectedTime = 0;

            // 获取方块对象的引用
            if (blockObjects.TryGetValue(hitBlock.position, out GameObject hitObject))
            {
                hitObject.SetActive(false);

                // 启动协程，等待一定时间后再启用物体
                StartCoroutine(EnableObjectAfterDelay(hitBlock,hitObject, 15f));
            }

            // 保存修改后的方块数据
            SaveBlocks();
        }
    }

    private IEnumerator EnableObjectAfterDelay(BlockData hitBlock,GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 启用物体
        obj.SetActive(true);
        hitBlock.isCollected = true;
        
    }
}
