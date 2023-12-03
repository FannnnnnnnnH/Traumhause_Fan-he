using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * GravityAttractor.cs
 * 
 * 该脚本负责引力吸引功能，使指定的物体朝向并受到引力吸引到脚本所附加的游戏对象。
 * 
 * 公共属性：
 * - gravity: 引力的强度，控制物体被吸引的程度。
 * 
 * 方法：
 * - Attract(Transform body): 吸引物体朝向并施加引力。
 * 作者: [Sebastian Lague] 参考 https://www.youtube.com/watch?v=TicipSVT-T8&t=1508s
 * 代码修改过:[FanHAHA]
 * 日期: [日期]
 */
public class GravityAttractor : MonoBehaviour
{
    public float gravity = -20f;
    Rigidbody rb;
    public void Attract(Transform body)
    {
        Vector3 targetPlayer = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        body.rotation = Quaternion.FromToRotation(bodyUp, targetPlayer)*body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetPlayer * gravity);
    }
    // Start is called before the first frame update
   
}
