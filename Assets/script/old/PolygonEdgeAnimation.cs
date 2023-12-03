using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonEdgeAnimation : MonoBehaviour
{
    public float waveFrequency = 1.0f; // 波的频率
    public float waveScale = 0.1f; // 波的幅度
    public float waveSpeed = 1.0f; // 波的速度

    private Mesh originalMesh; // 原始的Mesh数据，用于还原顶点位置

    void Start()
    {
        // 获取多面体的Mesh
        originalMesh = GetComponent<MeshFilter>().mesh;

        // 启动协程，控制顶点随机位移
        StartCoroutine(RandomVertexDisplacement());
    }

    IEnumerator RandomVertexDisplacement()
    {
        while (true)
        {
            // 克隆原始Mesh，用于随机位移
            Mesh clonedMesh = Instantiate(originalMesh);

            // 获取多面体的顶点数据
            Vector3[] vertices = clonedMesh.vertices;

            // 根据时间和Perlin噪声生成随机位移
            float time = Time.time * waveSpeed;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = vertices[i];

                // 在x、y、z轴上分别应用Perlin噪声来生成随机位移
                float offsetX = Mathf.PerlinNoise(vertex.x * waveFrequency, time) * waveScale;
                float offsetY = Mathf.PerlinNoise(vertex.y * waveFrequency, time) * waveScale;
                float offsetZ = Mathf.PerlinNoise(vertex.z * waveFrequency, time) * waveScale;

                vertex.x += offsetX;
                vertex.y += offsetY;
                vertex.z += offsetZ;

                vertices[i] = vertex;
            }

            // 更新Mesh的顶点数据
            clonedMesh.vertices = vertices;
            clonedMesh.RecalculateNormals();

            // 将随机位移后的Mesh应用到多面体上
            GetComponent<MeshFilter>().mesh = clonedMesh;

            // 等待一秒后再次进行随机位移
            yield return new WaitForSeconds(1.0f);
        }
    }
}
