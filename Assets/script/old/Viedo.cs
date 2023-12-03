using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Viedo : MonoBehaviour
{
    private RaycastHit hit;
    public Camera ca;
    private Ray ra;
    VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        Debug.Assert(videoPlayer);
        videoPlayer.Stop();
    }

    void Update()
    {
        //判断是否按下左键
        if (Input.GetMouseButtonDown(0))
        {
            //发射线   
            ra = ca.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit))
            {
                //我要移动的物体名字叫Sphere
                if (hit.transform.name == "Plane")
                {
                    if (videoPlayer.isPlaying) { videoPlayer.Pause();
                    }else
                    // hit.collider.gameObject.transform.position = player
                    videoPlayer.Play();
                }
            }
        }
        
    }
}
