using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigg_1 : MonoBehaviour
{

    private Door_1 m_Door;

    private bool enterCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Door = GameObject.Find("toer_1").GetComponent<Door_1>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (m_Door.GetIsOpen())
                {
                    m_Door.closeDoor();

                }
                else
                {
                    m_Door.openDoor();
                }
            }
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Debug.Log("enter");

            enterCollide = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            Debug.Log("exit");

            enterCollide = false;
        }

    }
}
