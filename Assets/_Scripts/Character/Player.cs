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

    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    
    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded = true;

    public delegate void PlayerInteraction(Player player);
    public PlayerInteraction interaction;

    private void Update()
    {
        HandleMovement();
        HandleGravity();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float horizontal = Movement.GetAxisDirection("Horizontal");
        float vertical = Movement.GetAxisDirection("Vertical");
        Vector3 direction = Movement.GetDirection(horizontal, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
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
            cameraController.CameraLookAt(this.transform);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<InteractiveObject>() && interaction == null)
        {
            InteractiveObject interactiveObject = hit.collider.GetComponent<InteractiveObject>();
            interaction = interactiveObject.Interaction;
        }
    }
}
