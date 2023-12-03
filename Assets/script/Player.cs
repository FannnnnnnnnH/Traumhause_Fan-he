using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player.cs
 * 
 * 该脚本控制玩家的移动、相机旋转和跳跃行为。
 * 还管理玩家的状态，例如在陆地上还是在水中。
 * 
 * 公共属性：
 * - playerSpeed: 玩家的移动速度。
 * - mouseSensitivityX: 相机水平旋转的鼠标灵敏度。
 * - mouseSensitivityY: 相机垂直旋转的鼠标灵敏度。
 * - jumpForce: 玩家跳跃时施加的力。
 * - currentState: 玩家的当前状态（OnLand 或 OnSea）。
 * 
 * 私有属性：
 * - rb: 玩家的 Rigidbody 组件引用。
 * - cameraT: 主相机的 Transform 组件引用。
 * - verticalLookRotation: 相机的当前垂直旋转。
 * - isPlay: 标志，指示玩家是否处于可玩状态。
 * 
 * 方法：
 * - Start: Called before the first frame update, initializes player components and state.
 * - Update: Called once per frame, handles player input and updates player state.
 * - HandleMovement: Handles player movement and camera rotation.
 * - HandleJumping: Handles player jumping behavior.
 * - HandleUI: Handles UI interactions, such as toggling a bag.
 * - FixedUpdate: Called at a fixed time step, updates player's rigidbody position.
 * - DisableMove: Disables the player's ability to move.
 * - EnableMove: Enables the player's ability to move.
 * - Start: 在第一帧更新之前调用，初始化玩家组件和状态。
 * - HandleMovement: 处理玩家移动和相机旋转。
 * - HandleJumping: 处理玩家跳跃行为。
 * - HandleUI: 处理 UI 交互，如切换背包。
 * - FixedUpdate: 在固定时间步长调用，更新玩家的 Rigidbody 位置。
 * - DisableMove: 禁用玩家的移动能力。
 * - EnableMove: 启用玩家的移动能力。
 * 
 * 作者: [Sebastian Lague] 参考 https://www.youtube.com/watch?v=TicipSVT-T8&t=1508s
 * 代码修改过:[FanHAHA]
 * 日期: [日期]
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
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);//平滑移动

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
