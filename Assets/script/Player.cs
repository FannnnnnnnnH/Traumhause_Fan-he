using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player.cs
 * 
 * �ýű�������ҵ��ƶ��������ת����Ծ��Ϊ��
 * ��������ҵ�״̬��������½���ϻ�����ˮ�С�
 * 
 * �������ԣ�
 * - playerSpeed: ��ҵ��ƶ��ٶȡ�
 * - mouseSensitivityX: ���ˮƽ��ת����������ȡ�
 * - mouseSensitivityY: �����ֱ��ת����������ȡ�
 * - jumpForce: �����Ծʱʩ�ӵ�����
 * - currentState: ��ҵĵ�ǰ״̬��OnLand �� OnSea����
 * 
 * ˽�����ԣ�
 * - rb: ��ҵ� Rigidbody ������á�
 * - cameraT: ������� Transform ������á�
 * - verticalLookRotation: ����ĵ�ǰ��ֱ��ת��
 * - isPlay: ��־��ָʾ����Ƿ��ڿ���״̬��
 * 
 * ������
 * - Start: Called before the first frame update, initializes player components and state.
 * - Update: Called once per frame, handles player input and updates player state.
 * - HandleMovement: Handles player movement and camera rotation.
 * - HandleJumping: Handles player jumping behavior.
 * - HandleUI: Handles UI interactions, such as toggling a bag.
 * - FixedUpdate: Called at a fixed time step, updates player's rigidbody position.
 * - DisableMove: Disables the player's ability to move.
 * - EnableMove: Enables the player's ability to move.
 * - Start: �ڵ�һ֡����֮ǰ���ã���ʼ����������״̬��
 * - HandleMovement: ��������ƶ��������ת��
 * - HandleJumping: ���������Ծ��Ϊ��
 * - HandleUI: ���� UI ���������л�������
 * - FixedUpdate: �ڹ̶�ʱ�䲽�����ã�������ҵ� Rigidbody λ�á�
 * - DisableMove: ������ҵ��ƶ�������
 * - EnableMove: ������ҵ��ƶ�������
 * 
 * ����: [Sebastian Lague] �ο� https://www.youtube.com/watch?v=TicipSVT-T8&t=1508s
 * �����޸Ĺ�:[FanHAHA]
 * ����: [����]
 */

public class Player : MonoBehaviour
{
    //Player Features
     [Header("Movement Parameters")]
    public float playerSpeed = 5f;
    public float playerRotateSpeed = 150f;
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float jumpForce = 10;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public GameObject rayCenter;

    [Header("Other Components")]
    private Rigidbody rb;
    public Transform cameraT;
    public GameObject shiff;
    public GameObject myBagUI;

    private bool canMove = true;
    private bool isPlay;
    private bool grounded;
    private float gravity = -100f;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private float verticalLookRotation;

    public enum State
    {
        OnLand,
        OnSea
    }
    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        isPlay = false;
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true; //
        myBagUI.SetActive(isPlay);
    }

    // Update is called once per frame
    void Update()
    {
        HandleGroundState();

        switch (currentState)
        {
            case State.OnLand:
                shiff.gameObject.SetActive(false);
                HandleMovement();
                HandleJumping();
                break;
            case State.OnSea:
                shiff.gameObject.SetActive(true);
                HandleMovement();
                break;
        }

        HandleUI();
    }
    void HandleGroundState()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.CompareTag("wasser"))
            {
                currentState = State.OnSea;
                shiff.SetActive(true);
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                currentState = State.OnLand;
                shiff.SetActive(false);
            }
        }
    }
   
    

    void HandleMovement()
    {
        if (!canMove) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        Vector3 targetMoveAmount = moveDir * playerSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);//ƽ���ƶ�

        this.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void HandleJumping()
    {
        if (!canMove) return;
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        grounded = false;
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.85f, groundLayer))
        {
            grounded = true;
        }
    }

    void HandleUI()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isPlay = !isPlay;
            myBagUI.SetActive(isPlay);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }

    public void DisableMove()
    {
        canMove = false;
    }
    public void EnableMove()
    {
        canMove = true;
    } 
}
