using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Get input for movement and rotation
    public float verticalInput;
    public float horizontalInput;
    public Animator anim;
    public int myhealth = 100;
    CharacterController characterController;
    public 
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
       // characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {

      //  Debug.Log(characterController.detectCollisions);
    }
    public void Nimegongwa()
    {
        Debug.Log("Mkobaman Amenigonga");
    }
    void Songa()
    {

    }
    public void Reaction(string commandString)
    {
        switch (commandString)
        {
            case "Kichwa":
                anim.SetTrigger("Punched");
                break;
            case "KwaGroin":
                anim.SetTrigger("GroinKick");
                myhealth -=8;
                break;
            case "PowerUp":
                
                break;
            default:
                Debug.LogWarning("Unknown trigger: " + commandString);
                break;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
    }


}

