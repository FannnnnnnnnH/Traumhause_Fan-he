using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartiSystem : MonoBehaviour
{
    private RaycastHit hit;
    public Camera ca;
    private Ray ra;
    public ParticleSystem pa;
    // Start is called before the first frame update
    void Start()
    {
        pa = gameObject.GetComponent<ParticleSystem>();
        pa.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //发射线   
            ra = ca.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit))
            {
                //我要移动的物体名字叫Sphere
                if (hit.transform.name == "feuer")
                {
                    if (pa.isPlaying)
                    {
                        pa.Stop();
                    }
                    else
                        // hit.collider.gameObject.transform.position = player
                        pa.Play();
                }
            }
        }
    }
}
