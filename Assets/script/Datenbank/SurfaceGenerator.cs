using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SurfaceGenerator.cs
 * 
 * 该脚本负责曲面上预制体的生成和管理。
 * 
 * 
 * 公共属性：
 * - squarePrefab: 方块预制体变量。
 * - surfaceObject: 曲面对象，用于获取顶点信息。
 * - numberOfSquares: 生成正方形的数量。
 * - persistentContainer: 用于永久化正方形的容器对象。
 * - blockManager: 与 BlockManager 脚本交互，用于保存和加载方块数据。
 * - blumenPefab: 花朵预制体变量（未使用）。
 * 
 * 方法：
 * - Start: 在第一帧更新之前调用。当前未用于加载花朵。
 * - Update: 在每一帧更新时调用。检测用户输入，执行生成花朵、加载方块和保存方块等操作。
 * - GenerBlumen: 在曲面的随机顶点位置生成花朵的方法。
 * - AddToSquares: 将永久化容器中的子物体信息添加到 BlockManager 的方块列表中，以实现永久化。
 * - LoadSquares: 根据 BlockManager 中的方块数据加载并实例化方块。
 * 
 * 作者：[Fan He]
 * 日期：[日期]
 */

public class SurfaceGenerator : MonoBehaviour
{
    //public GameObject test;
    public GameObject squarePrefab;
    public GameObject surfaceObject;
    private int numberOfSquares = 10;
    public GameObject persistentContainer;
    public BlockManager blockManager;
    public GameObject blumenPefab;
    public bool isNochDa=true;
    public float lastCollectedTime=15f;



    void Start()
    {
        //blockManager = FindObjectOfType<BlockManager>();

        //if (blockManager == null)
        //{
        //    Debug.LogError("BlockManager not found in the scene. Make sure it is present.");
        //    return;
        //}
        //LoadSquares();

    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    GenerBlumen();
        //}
        if (Input.GetKeyDown(KeyCode.V))
        {
            //LoadSquares();
            blockManager.LoadBlocks(); ;
        }
        //调整花朵之后再次保存花朵
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (blockManager.list.Count == 0)
            {
                //AddToSquares();
                blockManager.SaveBlocks();
            }
        }

    }

    /// <summary>
    /// 随机在顶点生成花朵
    /// </summary>
    void GenerBlumen()
    {
       
        MeshFilter meshFilter = surfaceObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("Surface object does not have a MeshFilter component.");
            return;
        }

        // 获取曲面的顶点信息
        Vector3[] vertices = meshFilter.mesh.vertices;
        Vector3[] normals = meshFilter.mesh.normals;


        // 使用HashSet来存储已选择的顶点索引
        HashSet<int> selectedVertices = new HashSet<int>();

        // 随机生成指定数量的正方形
        for (int i = 0; i < numberOfSquares; i++)
        {
            // 随机选择一个曲面上的顶点
            int randomVertexIndex;
            Vector3 randomVertex;

            do
            {
                randomVertexIndex = Random.Range(0, vertices.Length);
                randomVertex = surfaceObject.transform.TransformPoint(vertices[randomVertexIndex]);
            } while (selectedVertices.Contains(randomVertexIndex));

            // 将已选择的顶点索引添加到HashSet中
            selectedVertices.Add(randomVertexIndex);

            // 在顶点的位置创建正方形
            GameObject square = Instantiate(squarePrefab, randomVertex, Quaternion.identity);
            square.transform.SetParent(persistentContainer.transform);
            Vector3 vertexNormal = surfaceObject.transform.TransformDirection(normals[randomVertexIndex]);

            // 将方块旋转朝向曲面的法线
            square.transform.rotation = Quaternion.FromToRotation(Vector3.up, vertexNormal);
        }
    }
    /// <summary>
    /// 花朵永久化
    /// </summary>
    void AddToSquares()
    {
        blockManager.list.Clear();

        //子物体列表
        List<GameObject> childrenList = new List<GameObject>();
       
        for (int i = 0; i < persistentContainer.transform.childCount; i++)
        {
            GameObject child = persistentContainer.transform.GetChild(i).gameObject;
            childrenList.Add(child);
            Debug.Log("Child Position: " + child.transform.position);
            Debug.Log("Child Rotation: " + child.transform.rotation);

           

            blockManager.list.Add(new BlockData(child.transform.position,child.transform.rotation,isNochDa,lastCollectedTime));
            Debug.LogError("Surface");
        }
       
    }
    /// <summary>
    /// 加载花朵
    /// </summary>
    void LoadSquares()
    {
         GameObject squarePrefab = blockManager.squarePrefab; // 获取方块预制体

        if (blockManager!=null)
        {
            
            Debug.Log("开始加载");
            // 根据BlockManager中的方块数据生成方块
            foreach (BlockData blockData in blockManager.list)
            {
                GameObject square = Instantiate(squarePrefab, blockData.position, blockData.rotation);

                Debug.Log(square.transform.position);
                // 设置方块的其他属性...
            }
        }
        else { Debug.Log("没有发现文件"); }
    }
}
