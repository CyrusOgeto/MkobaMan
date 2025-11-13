using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MkobaMover : MonoBehaviour
{
    // Get input for movement and rotation
    public float verticalInput;
    public float horizontalInput;
    public Animator anim;
    CharacterController characterController;
    public float runspeed;
    public float rotationSpeed;
    public float gravity = -20f;
    public float jumpSpeed = 15;
    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    public MkobaController mkobaController;

    public bool MovingToTarget = false;

    public GameObject Anazua; // Variable to store the current target
    public GameObject blueSphere; // GameObject to represent the blue sphere

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PickTarget();
        Songa();
        Fikia();
    }

    void Fikia()
    {
        if (MovingToTarget && Anazua != null)
        {
            Vector3 targetPosition = Anazua.transform.position;
            targetPosition.y = transform.position.y; // Keep the same height
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Move the character towards the target position
            characterController.Move(moveDirection * runspeed * Time.deltaTime);

            // Rotate towards the target
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check the distance to the target
            if (Vector3.Distance(transform.position, targetPosition) < mkobaController.AttackDistance)
            {
                // Move back if within attack distance
                Vector3 moveBackDirection = (transform.position - targetPosition).normalized;
                characterController.Move(moveBackDirection * runspeed * Time.deltaTime);

                // Optionally, you can add logic here for when the character reaches the target
                // For example, you can set Anazua to null to stop moving towards it
                mkobaController.Move();
                MovingToTarget = false;
                mkobaController.acting = false;
            }
        }
    }

    public void PickTarget()
    {
        var forward = transform.forward;
        var right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 inputDirection = forward; // Assume looking forward for now, update this based on actual input

        RaycastHit hitInfo;
        float sphereRadius = 3f;
        float maxDistance = 35f;

        // Draw a debug line to visualize the SphereCast direction
        Debug.DrawLine(transform.position, transform.position + inputDirection * maxDistance, Color.blue);

        if (Physics.SphereCast(transform.position, sphereRadius, inputDirection, out hitInfo, maxDistance))
        {
            if (hitInfo.collider.gameObject.tag == "Adui")
            {
                Anazua = hitInfo.collider.gameObject;
            }

        }
    }

    void Songa()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * runspeed * verticalInput;
            turnVelocity = transform.up * rotationSpeed * horizontalInput;
            //anim.SetFloat("Vertical", verticalInput);
            if (Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpSpeed;
            }
        }
        //Adding Gravity
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
        if (mkobaController.acting)
        {
            MovingToTarget = true;
        }
    }
}
