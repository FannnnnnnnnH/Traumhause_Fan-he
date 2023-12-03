using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SurfaceGenerator.cs
 * 
 * �ýű�����������Ԥ��������ɺ͹���
 * 
 * 
 * �������ԣ�
 * - squarePrefab: ����Ԥ���������
 * - surfaceObject: ����������ڻ�ȡ������Ϣ��
 * - numberOfSquares: ���������ε�������
 * - persistentContainer: �������û������ε���������
 * - blockManager: �� BlockManager �ű����������ڱ���ͼ��ط������ݡ�
 * - blumenPefab: ����Ԥ���������δʹ�ã���
 * 
 * ������
 * - Start: �ڵ�һ֡����֮ǰ���á���ǰδ���ڼ��ػ��䡣
 * - Update: ��ÿһ֡����ʱ���á�����û����룬ִ�����ɻ��䡢���ط���ͱ��淽��Ȳ�����
 * - GenerBlumen: ��������������λ�����ɻ���ķ�����
 * - AddToSquares: �����û������е���������Ϣ��ӵ� BlockManager �ķ����б��У���ʵ�����û���
 * - LoadSquares: ���� BlockManager �еķ������ݼ��ز�ʵ�������顣
 * 
 * ���ߣ�[Fan He]
 * ���ڣ�[����]
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
        //��������֮���ٴα��滨��
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
    /// ����ڶ������ɻ���
    /// </summary>
    void GenerBlumen()
    {
       
        MeshFilter meshFilter = surfaceObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("Surface object does not have a MeshFilter component.");
            return;
        }

        // ��ȡ����Ķ�����Ϣ
        Vector3[] vertices = meshFilter.mesh.vertices;
        Vector3[] normals = meshFilter.mesh.normals;


        // ʹ��HashSet���洢��ѡ��Ķ�������
        HashSet<int> selectedVertices = new HashSet<int>();

        // �������ָ��������������
        for (int i = 0; i < numberOfSquares; i++)
        {
            // ���ѡ��һ�������ϵĶ���
            int randomVertexIndex;
            Vector3 randomVertex;

            do
            {
                randomVertexIndex = Random.Range(0, vertices.Length);
                randomVertex = surfaceObject.transform.TransformPoint(vertices[randomVertexIndex]);
            } while (selectedVertices.Contains(randomVertexIndex));

            // ����ѡ��Ķ���������ӵ�HashSet��
            selectedVertices.Add(randomVertexIndex);

            // �ڶ����λ�ô���������
            GameObject square = Instantiate(squarePrefab, randomVertex, Quaternion.identity);
            square.transform.SetParent(persistentContainer.transform);
            Vector3 vertexNormal = surfaceObject.transform.TransformDirection(normals[randomVertexIndex]);

            // ��������ת��������ķ���
            square.transform.rotation = Quaternion.FromToRotation(Vector3.up, vertexNormal);
        }
    }
    /// <summary>
    /// �������û�
    /// </summary>
    void AddToSquares()
    {
        blockManager.list.Clear();

        //�������б�
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
    /// ���ػ���
    /// </summary>
    void LoadSquares()
    {
         GameObject squarePrefab = blockManager.squarePrefab; // ��ȡ����Ԥ����

        if (blockManager!=null)
        {
            
            Debug.Log("��ʼ����");
            // ����BlockManager�еķ����������ɷ���
            foreach (BlockData blockData in blockManager.list)
            {
                GameObject square = Instantiate(squarePrefab, blockData.position, blockData.rotation);

                Debug.Log(square.transform.position);
                // ���÷������������...
            }
        }
        else { Debug.Log("û�з����ļ�"); }
    }
}
