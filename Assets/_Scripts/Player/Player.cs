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
    bool isGrounded = true;
    Ledge activeLedge;

    public delegate void PlayerInteraction(Player player);
    public PlayerInteraction interaction;

    private void Update()
    {
        HandlePlayerMovement();
        HandleGravity();
        HandleInteraction();
    }

    private void HandlePlayerMovement()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                animationStateController.StateControl();
                HandleMovement();
                break;
            case PlayerState.MOVING:
                animationStateController.StateControl("isMoving");
                HandleMovement();
                break;
            case PlayerState.PUSHING:
                animationStateController.StateControl("isPushing");
                HandlePush();
                break;
            case PlayerState.PULLING:
                animationStateController.StateControl("isPulling");
                HandlePush();
                break;
            case PlayerState.CLIMBING:
                break;
            case PlayerState.JUMPING:
                break;
            case PlayerState.CROUCHING:
                animationStateController.StateControl("isCrouching");
                break;
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = Movement.GetDirection();

        if (direction.magnitude >= 0.01f)
        {
            playerState = PlayerState.MOVING;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            playerState = PlayerState.IDLE;
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
        else
        {
            playerState = PlayerState.IDLE;
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
            cameraController.Recenter(false);
        }
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleClimb(Vector3 handPosition, Ledge currentLedge)
    {
        activeLedge = currentLedge;
        StartCoroutine(ClimbRoutine(handPosition));
    }

    private IEnumerator ClimbRoutine(Vector3 handPosition)
    {
        controller.enabled = false;
        transform.position = handPosition;
        yield return new WaitForSeconds(0.2f);
        animationStateController.StateControl("IsClimbing");
        yield return new WaitForSeconds(0.2f);
    }

    public void ClimbLedge()
    {
        transform.position = activeLedge.GetStandPosition();
        controller.enabled = true;
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
