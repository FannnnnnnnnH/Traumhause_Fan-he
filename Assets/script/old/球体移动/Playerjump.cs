using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerjump : MonoBehaviour
{
    public static bool isObjectCollision;


    //�����Ծ���ٶ�
    public float jumpSpeed = 3;
    public float gSpeed = 9f;
    private float nowJumpSpeed;
    
    //����Ƿ�Ӵ�����
    public bool isGround=true;
    //���������ľ���
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
        // �����Ծ����
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            print("������Խ");
            isGround = false;
            isJump = true;
            nowJumpSpeed = jumpSpeed;
        }
        if (isJump)
        {
            nowJumpSpeed -= gSpeed * Time.deltaTime;
            this.transform.Translate(Vector3.up * nowJumpSpeed * Time.deltaTime);
            //����
           
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

            // ������ײ�����������������֮��ĽǶ�
            float angleUp = Vector3.Angle(contactNormal, Vector3.up);
            float angleDown = Vector3.Angle(contactNormal, Vector3.down);
            float angleForward = Vector3.Angle(contactNormal, Vector3.forward);
            float angleBack = Vector3.Angle(contactNormal, Vector3.back);
            float angleRight = Vector3.Angle(contactNormal, Vector3.right);
            float angleLeft = Vector3.Angle(contactNormal, Vector3.left);

            // �ҵ���С�Ƕȶ�Ӧ�ķ���
            float minAngle = Mathf.Min(angleUp, angleDown, angleForward, angleBack, angleRight, angleLeft);
            if (minAngle == angleUp)
            {
                Debug.Log("�ϰ������Ϸ�");
            }
            else if (minAngle == angleDown)
            {
                Debug.Log("�ϰ������·�");
            }
            else if(minAngle == angleForward)
            {
                Debug.Log("�ϰ�����ǰ��");
            }
            else if (minAngle == angleBack)
            {
                Debug.Log("�ϰ����ں�");
            }
            else if (minAngle == angleRight)
            {
                Debug.Log("�ϰ������Ҳ�");
            }
            else if (minAngle == angleLeft)
            {
                Debug.Log("�ϰ��������");
            }
        }
        // ����Ƿ���½�ڵ�����
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
        // ����Ƿ��뿪����
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
