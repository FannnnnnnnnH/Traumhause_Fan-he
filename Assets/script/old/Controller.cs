using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.collider.gameObject;    //获得选中物体
                string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                print(goName);
            }
        }
    }

}
