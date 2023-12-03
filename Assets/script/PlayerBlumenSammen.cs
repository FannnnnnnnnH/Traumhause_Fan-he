using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlumenSammen : MonoBehaviour
{
    public float interactionRange = 10f;
    public BlockManager blockManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInteractions();
        }
    }

    void HandleInteractions()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // ���� BlockManager �������߼�
            blockManager.HandleBlockInteraction(hit.transform.position);
        }
    }


}
