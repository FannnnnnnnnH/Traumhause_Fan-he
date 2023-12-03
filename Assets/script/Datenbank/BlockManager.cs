using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * BlockManager.cs
 * 
 * �ýű�������Ϸ�з������ݵ����л�������ͼ��ء�
 * �����������л��������ݵ��ࡢ���ڴ洢����ʵ�����б��Լ�ʹ�� JSON ����ͼ��ط���ķ�����
 * 
 * �������ԣ�
 * - list: �洢��ʾ����λ�ú���ת�� BlockData ʵ�����б�
 * - squarePrefab: ���� GameObject ��Ԥ���������
 * 
 * ������
 * - Start: �ڵ�һ֡����֮ǰ���á���ǰδ���ڼ��ػ��䡣
 * - SaveBlocks: ͨ������ת��Ϊ JSON ��д���ļ������淽�����ݡ�
 * - LoadBlocks: �� JSON �ļ����ر���ķ������ݲ�ʵ�������� GameObject��
 * 
 * �ࣺ
 * - BlockData: ��ʾ����λ�ú���ת���ࡣ�������л���
 * - BlockDataList: �������л��� BlockData ʵ���б�ĳ����ࡣ
 * 
 * ���ߣ�[�������]
 * ���ڣ�[����]
 */

//���л�,���л���ָ�洢�ͻ�ȡ�����ļ����ڴ�������ط��еĶ���
[System.Serializable]
public class BlockData
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isCollected;
    public float lastCollectedTime;
    // ������������...

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
    //����Ҫע��д�� ��public List<BlockData> blockDataList = new List<BlockData>();��ʵ��һ������
    public List<BlockData> list = new List<BlockData>();
    public GameObject squarePrefab; // ��ӷ���Ԥ�������
    public float regrowIntervalSeconds = 30f;

    private Dictionary<Vector3, GameObject> blockObjects = new Dictionary<Vector3, GameObject>();

    //public static List<BlockManager> allBlockManagers = new List<BlockManager>();

    private void Awake()
    {
        // �� Awake ��������� allBlockManagers �б�
        //allBlockManagers.Add(this);
    }
    private void Start()
    {
        //���ػ���
        LoadBlocks();
    }


        /// <summary>
        /// ����ֻ����һ��
        /// </summary>
        public void SaveBlocks()
    {
        BlockDataList blockDataList = new BlockDataList();
        blockDataList.blockDataList.AddRange(list); ;


        // ����������ת��Ϊ�ɱ���ĸ�ʽ������ JSON
        string json = JsonUtility.ToJson(blockDataList);

        // ������д�뵽�ļ���
        string filePath = Application.persistentDataPath + "/blockData.json";
        File.WriteAllText(filePath, json);

        Debug.Log("Blocks saved successfully.");

       
        Debug.Log("���سɹ�");
        //blockDataList =new blo
        //File.WriteAllText(filePath, json);
        Debug.Log("--------------------------------- ");
    }

    public void LoadBlocks()
    {
        // ���ļ��ж�ȡ���������
        string filePath = Application.persistentDataPath + "/blockData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // ����ȡ������ת���ط��������б�
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
        // ����Ƿ������˷���
        BlockData hitBlock = list.Find(block => block.position == hitPosition);

        if (hitBlock != null)
        {
            // �޸� isCollected ����
            hitBlock.isCollected = false;
            //hitBlock.lastCollectedTime = 0;

            // ��ȡ������������
            if (blockObjects.TryGetValue(hitBlock.position, out GameObject hitObject))
            {
                hitObject.SetActive(false);

                // ����Э�̣��ȴ�һ��ʱ�������������
                StartCoroutine(EnableObjectAfterDelay(hitBlock,hitObject, 15f));
            }

            // �����޸ĺ�ķ�������
            SaveBlocks();
        }
    }

    private IEnumerator EnableObjectAfterDelay(BlockData hitBlock,GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ��������
        obj.SetActive(true);
        hitBlock.isCollected = true;
        
    }
}
