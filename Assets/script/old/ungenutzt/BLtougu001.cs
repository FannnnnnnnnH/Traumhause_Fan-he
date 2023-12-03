using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLtougu001 : MonoBehaviour
{
    // Start is called before the first frame update

    public BLTriggerTouGu002 m_BLTriggerTouGu002;

    public float mousestate = 0f;

    public Vector3 V3;

    public Camera cam;

    RaycastHit hit;
    // Use this for initialization

    // Update is called once per frame
    void Start()
    {
        V3 = transform.position;
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0) && hit.transform.gameObject.name == "tougu002")
        {
            mousestate = 1;
        }



        else if (Input.GetMouseButtonUp(0) && m_BLTriggerTouGu002.tougustate == 0)
        {
            mousestate = 0;
            transform.position = V3;

        }
        else if (Input.GetMouseButtonUp(0) && m_BLTriggerTouGu002.tougustate == 1)
        {
            mousestate = 0;

        }


        if (mousestate == 1 && m_BLTriggerTouGu002.tougustate != 2)
        {
            Vector3 Pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Pos.z);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);



        }

    }




}
