using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrankTrigg : MonoBehaviour
{
    // Start is called before the first frame update
    private schrank m_Schrank;

    private bool enterCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Schrank = GameObject.Find("door_1").GetComponent<schrank>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (m_Schrank.GetIsOpen())
                {
                    m_Schrank.closeDoor();

                }
                else
                {
                    m_Schrank.openDoor();
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
