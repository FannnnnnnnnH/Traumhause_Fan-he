using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerjump : MonoBehaviour
{
    public static bool isObjectCollision;


    //玩家跳跃初速度
    public float jumpSpeed = 3;
    public float gSpeed = 9f;
    private float nowJumpSpeed;
    
    //玩家是否接触地面
    public bool isGround=true;
    //玩家离物体的距离
    public float obstacleDetectionDistance = 1f;
    private bool isJump;

    private void Awake()
    {
        //bodyTramsform = this.transform.Find("body");
        // rb = bodyTramsform.GetComponent<Rigidbody>();
        isObjectCollision = false;
        isGround = true;
    }

    private void Update()
    {
        // 检测跳跃输入
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            print("按下跳越");
            isGround = false;
            isJump = true;
            nowJumpSpeed = jumpSpeed;
        }
        if (isJump)
        {
            nowJumpSpeed -= gSpeed * Time.deltaTime;
            this.transform.Translate(Vector3.up * nowJumpSpeed * Time.deltaTime);
            //下落
           
            print(this.transform.localPosition.y);
            //this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            if (this.transform.localPosition.y <= 0)
            {
                isGround = true;
                isJump = false;
                this.transform.localPosition = Vector3.zero;

            }
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 contactNormal = contact.normal;

            // 计算碰撞法线与各个方向向量之间的角度
            float angleUp = Vector3.Angle(contactNormal, Vector3.up);
            float angleDown = Vector3.Angle(contactNormal, Vector3.down);
            float angleForward = Vector3.Angle(contactNormal, Vector3.forward);
            float angleBack = Vector3.Angle(contactNormal, Vector3.back);
            float angleRight = Vector3.Angle(contactNormal, Vector3.right);
            float angleLeft = Vector3.Angle(contactNormal, Vector3.left);

            // 找到最小角度对应的方向
            float minAngle = Mathf.Min(angleUp, angleDown, angleForward, angleBack, angleRight, angleLeft);
            if (minAngle == angleUp)
            {
                Debug.Log("障碍物在上方");
            }
            else if (minAngle == angleDown)
            {
                Debug.Log("障碍物在下方");
            }
            else if(minAngle == angleForward)
            {
                Debug.Log("障碍物在前方");
            }
            else if (minAngle == angleBack)
            {
                Debug.Log("障碍物在后方");
            }
            else if (minAngle == angleRight)
            {
                Debug.Log("障碍物在右侧");
            }
            else if (minAngle == angleLeft)
            {
                Debug.Log("障碍物在左侧");
            }
        }
        // 检测是否着陆在地面上
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGround = true;
        //}
        //else if (collision.gameObject.CompareTag("Object"))
        //{
        //    isObjectCollision = true;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        // 检测是否离开地面
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isGround = false;
        //}
        //else if (collision.gameObject.CompareTag("Object"))
        //{
        //    isObjectCollision = false;
        //}
    }
}
