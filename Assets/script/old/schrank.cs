﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schrank : MonoBehaviour
{
    private Transform m_Transform;
    private bool isOpen = false;
    Vector3 m;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m = m_Transform.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openDoor()
    {
       
        m_Transform.Rotate(0, -180, 0);
        isOpen = true;
        print("这kai");
    }

    public void closeDoor()
    {
        m_Transform.Rotate(0, 180, 0);
        isOpen = false;
        print("这关");

    }


    public bool GetIsOpen()
    {
        return isOpen;
    }
    public void SetIsOpen(bool b)
    {
        isOpen = b;


    }
}