using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMesh : MonoBehaviour
{
    public Mesh customMesh; // ����ָ������Զ���Mesh

    void Start()
    {
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        MeshCollider col = this.GetComponent<MeshCollider>();
        col.sharedMesh = mesh;
    }
}
