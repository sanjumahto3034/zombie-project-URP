using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovementManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    public GameObject playerVisionCamera;
    public float walkSpeed, runningSpeed, sensitiyvity, maxForce, jumpForce;
    private Vector2 moveDirection, lookDirection;
    public float lookSpeed;
    private float lookRotation;
    public CommonInput commonInput;
    public float gravityValue = 1;
    private float rotationX;

    private float lookXLimit = 60;

    private bool isGrounded = true;

    private bool isRunning = false;

    private PlayerAnimationController animationController;
    public FloatingJoystick joystick;

    private int globalFireTouchControl = 0;

    private int canUserSwipeScreen = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        commonInput = new CommonInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animationController = GetComponent<PlayerAnimationController>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    private void LateUpdate()
    {
        rotationX += -lookDirection.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerVisionCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, lookDirection.x * lookSpeed, 0);
        lookDirection = Vector2.zero;
    }
    private void MovePlayer()
    {

        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Method Work -> Responsible for walking and running movement of player
        */

        float moveSpeed = (isRunning) ? runningSpeed : walkSpeed;
        Vector3 currentVelocity = rb.velocity;
        if (constant.IS_NATIVE)
            MOBILE_TOUCH_CONTROL();


        Vector3 targetValocity = new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed;
        targetValocity = transform.TransformDirection(targetValocity);
        currentVelocity.y = gravityValue;
        Vector3 velocityChange = (targetValocity - currentVelocity);
        Vector3.ClampMagnitude(velocityChange, maxForce);



        rb.AddForce(velocityChange, ForceMode.VelocityChange);
        // moveAnimationManager(moveDirection);
        //Animation

        // Debug.Log(velocityChange + " : " + currentVelocity);
    }

    private void moveAnimationManager(Vector2 result)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        if (result != Vector2.zero && !isRunning) animationController.SetWalkAnimation(true);
        else if (result != Vector2.zero && isRunning) animationController.SetRunAnimation(true);
        else animationController.SetIdealAnimation(true);
    }

    public void JumpAction(InputAction.CallbackContext context)
    {
        /*
    * Get Input From Unity new Input System
    * @param callback context show phases of input
        */
        if (isGrounded)
        {
            Vector3 jumpFourceVector = Vector3.zero;
            jumpFourceVector = Vector3.up * jumpForce;
            rb.AddForce(jumpFourceVector, ForceMode.Impulse);
            // Debug.Log("[KEYBOARD PRESS -> SPACE]");
            SetGrounded(false);
        }
    }

    public void moveAction(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        moveDirection = context.ReadValue<Vector2>();
        // Debug.Log("[KEYBOARD PRESS -> WASD]");
    }
    public void mouseAction(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        if (canUserSwipeScreen == CAN_SWIPE) lookDirection = context.ReadValue<Vector2>();
        // Debug.Log("[MOUSE MOVED] -> " + lookDirection);
    }
    public void addRecoilOnPlayerCamera(Vector2 _recoil)
    {
        /*
        * @param Vector2 _recoil Amount at Y-Axis
        * Note need to reset to zero to reset back to no recoil from function =>(resetRecoil)
        * Add Custom recoil to the player gun
        * 
        */
        lookDirection += _recoil;
    }
    public void resetRecoil()
    {
        /*
        * This function will reset recoil of player gun
        * It is necessary to reset recoil after apply some amount of recoil 
        */
        lookDirection = Vector3.zero;
    }

    public void FireAction(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        // Debug.Log("[MOUSE EVENT LEFT CLICK]");
    }

    public void SetGrounded(bool state)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        isGrounded = state;
    }

    public void RunAction(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
        */
        isRunning = context.performed ? true : false;
    }



    public void MOBILE_CONTROL_JUMP()
    {
        /*
        * This function will try from canvas UI 
        */
        if (isGrounded)
        {
            Vector3 jumpFourceVector = Vector3.zero;
            jumpFourceVector = Vector3.up * jumpForce;
            rb.AddForce(jumpFourceVector, ForceMode.Impulse);
            // Debug.Log("[KEYBOARD PRESS -> SPACE]");
            SetGrounded(false);
        }
    }

    private void MOBILE_TOUCH_CONTROL()
    {
        // Debug.Log(TAG+Input.touchCount);
        if (Input.touchCount > 0)
        {
            moveDirection.x = joystick.Horizontal;
            moveDirection.y = joystick.Vertical;
            foreach (Touch touch in Input.touches)
            {
                // int _touchUiId = touch.fingerId;
                // // EventSystem.current.IsPointerOverGameObject(_touchUiId)
                // if (touch.deltaPosition.x > 300f)
                // {
                //     // canUserSwipeScreen = CAN_SWIPE;
                //     Debug.Log(TAG + "Touch Successfull");
                // }else{
                //     // canUserSwipeScreen = CAN_NOT_SWIPE;
                // }
            }
        }
    }
    public void MOBILE_CAMERA_MOVE_TOUCH_AREA(int isTouching){
        Debug.Log(TAG+isTouching);
        canUserSwipeScreen = isTouching;
    }

    private static string TAG = "[PlayerMovementManager] ";
    private static int CAN_SWIPE = 1;
    private static int CAN_NOT_SWIPE = 0;



}
