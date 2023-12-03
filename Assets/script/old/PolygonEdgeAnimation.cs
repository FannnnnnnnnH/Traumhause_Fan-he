using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonEdgeAnimation : MonoBehaviour
{
    public float waveFrequency = 1.0f; // ����Ƶ��
    public float waveScale = 0.1f; // ���ķ���
    public float waveSpeed = 1.0f; // �����ٶ�

    private Mesh originalMesh; // ԭʼ��Mesh���ݣ����ڻ�ԭ����λ��

    void Start()
    {
        // ��ȡ�������Mesh
        originalMesh = GetComponent<MeshFilter>().mesh;

        // ����Э�̣����ƶ������λ��
        StartCoroutine(RandomVertexDisplacement());
    }

    IEnumerator RandomVertexDisplacement()
    {
        while (true)
        {
            // ��¡ԭʼMesh���������λ��
            Mesh clonedMesh = Instantiate(originalMesh);

            // ��ȡ������Ķ�������
            Vector3[] vertices = clonedMesh.vertices;

            // ����ʱ���Perlin�����������λ��
            float time = Time.time * waveSpeed;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = vertices[i];

                // ��x��y��z���Ϸֱ�Ӧ��Perlin�������������λ��
                float offsetX = Mathf.PerlinNoise(vertex.x * waveFrequency, time) * waveScale;
                float offsetY = Mathf.PerlinNoise(vertex.y * waveFrequency, time) * waveScale;
                float offsetZ = Mathf.PerlinNoise(vertex.z * waveFrequency, time) * waveScale;

                vertex.x += offsetX;
                vertex.y += offsetY;
                vertex.z += offsetZ;

                vertices[i] = vertex;
            }

            // ����Mesh�Ķ�������
            clonedMesh.vertices = vertices;
            clonedMesh.RecalculateNormals();

            // �����λ�ƺ��MeshӦ�õ���������
            GetComponent<MeshFilter>().mesh = clonedMesh;

            // �ȴ�һ����ٴν������λ��
            yield return new WaitForSeconds(1.0f);
        }
    }
}
