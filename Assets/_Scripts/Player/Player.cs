using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundChecker;
    public LayerMask ground;
    public CameraController cameraController;
    public AnimationStateController animationStateController;
    public PlayerState playerState = 0;

    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    
    float turnSmoothVelocity;
    Vector3 velocity;
    [SerializeField] bool isGrounded = true;

    public delegate void PlayerInteraction(Player player);
    public PlayerInteraction interaction;

    private void Update()
    {
        if (playerState != PlayerState.CUTSCENE)
        {
            HandleInput();
            HandleInteraction();
            HandleGravity();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            animationStateController.StateControl("isJumping", true);
            HandleJump();
        }
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            animationStateController.StateControl("isMoving", true);
            HandleMovement();
        }
        else
        {
            animationStateController.StateControl("isMoving", false);
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = Movement.GetDirection();

        if (playerState == PlayerState.PUSHING || playerState == PlayerState.PULLING)
        {
            HandlePush();
            return;
        }
        else
        {
            animationStateController.StateControl("isPushing", false);
            animationStateController.StateControl("isPulling", false);
        }

        if (direction.magnitude >= 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    private void HandlePush()
    {
        Vector3 direction = Movement.GetDirection();
        
        if (direction.magnitude >= 0.1f)
        {
            transform.LookAt(cameraController.playerCam.LookAt);
            controller.Move(direction * Time.deltaTime);
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            interaction?.Invoke(this);
        }
        else
        {
            interaction = null;
            cameraController.CameraLookAt(this.transform);
            playerState = PlayerState.IDLE;
            cameraController.Recenter(false);
        }
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
            animationStateController.StateControl("isJumping", false);
        }
        velocity.y += gravity * Time.deltaTime;
         if (controller.enabled == true) controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<InteractiveObject>() && interaction == null)
        {
            InteractiveObject interactiveObject = hit.collider.GetComponent<InteractiveObject>();
            interaction = interactiveObject.Interaction;
        }
    }
}
