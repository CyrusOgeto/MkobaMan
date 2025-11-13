using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkobaMover : MonoBehaviour
{
    // Get input for movement and rotation
    public float verticalInput;
    public float horizontalInput;
    public Animator anim;
    CharacterController characterController;
    public float runspeed = 5f;
    public float rotationSpeed = 720f;
    public float gravity = -20f;
    public float jumpSpeed = 15f;
    public bool moveback = false;
    public GameObject stepbackpoint;
    private Vector3 moveVelocity;
    private Vector3 turnVelocity;
    public Camera mainCamera;

    
    public MkobaController mkobaController;

    public bool MovingToTarget = false;

    public GameObject Anazua; // Variable to store the current target
    public GameObject blueSphere; // GameObject to represent the blue sphere

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        mkobaController = GetComponentInChildren<MkobaController>();
        stepbackpoint.transform.position = this.transform.position - this.transform.forward * 15.5f;
    }

    // Update is called once per frame
    void Update()
    {
        fikia();
        Stepback();
    }

    void Songa()
    {        
        if (Anazua != null)
        {
            // Calculate the direction to Anazua
            Vector3 directionToTarget = Anazua.transform.position - transform.position;
            directionToTarget.y = 0; // Ignore vertical movement to keep the character on the ground

            
            if (directionToTarget.magnitude > 12f) // Ensure we have a significant direction
            {
                // Rotate the character to face the target
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                if (!moveback)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                // Move the character towards Anazua
                moveVelocity = directionToTarget.normalized * runspeed;
                moveVelocity.y = gravity; // Apply gravity to movement
                characterController.Move(moveVelocity * Time.deltaTime);
                anim.SetBool("Moving", true);
                MovingToTarget = true;
            }
            else
            {
                MovingToTarget = false;
                moveback = false;
                anim.SetBool("Moving", false);
            }
        }
    }

    void Chagua()
    {
        if (Anazua != null)
        {
            // Make the blueSphere a child of Anazua to follow it
            blueSphere.transform.parent = Anazua.transform;
            blueSphere.transform.localPosition = Vector3.zero; // Center the blueSphere around Anazua
            //mkobaController.Readiness();
        }
    }

    void Gonga()
    {
        if(Anazua!=null && !MovingToTarget)
        {
            Debug.Log("Gonga Mtu Vibaya Sana");//The Detector
            //mkobaController.move();
            Anazua = null;
        }
    }

    void fikia()
    {
        Chagua(); // Adjust the blueSphere position

        Songa();  // Move towards Anazua

        Gonga();
    }
    void Stepback()
    {
        
        if (moveback)
        {
            Anazua = stepbackpoint;
        }
        else
        {
            stepbackpoint.transform.position = this.transform.position - this.transform.forward * 15.5f;
        }
    }
}
