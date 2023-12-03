using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  BLTriggerTouGu002 : MonoBehaviour
{
    public BLtougu001 m_BLtougu001;
    public GameObject tougu;
    public float tougustate = 0f;
    private AudioSource audio_BLTriggerTouGu002;

    // Start is called before the first frame update
    void Start()
    {
        tougu = GameObject.Find("tougu001");
        audio_BLTriggerTouGu002 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        if (tougustate != 2)
            tougustate = 1;
        if (col.tag == "Tougu" && m_BLtougu001.mousestate == 0 && tougustate == 1)
        {

            tougu.transform.position = transform.position;
            tougustate = 2;
            transform.parent.SendMessage("CountBone", true);
            audio_BLTriggerTouGu002.Play();
        }
    }

    void OnTriggerExit(Collider col)

    {
        if (tougustate != 2)
        {
            tougustate = 0;
        }
    }

    
}
