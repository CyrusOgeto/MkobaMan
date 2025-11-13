using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public Animator anim;
    public bool Kasauti;
    public GameObject mainguy;
    private Vector3 moveVelocity;
    public float runspeed = 12f;
    public bool move;
    private float timer;
    public float decisionInterval = 2f;
    public float distancetoguy = 100f;



    public AudioSource AudioManager;
    public AudioClip PunchSound;

    public bool canact;
    public int myhealth = 100;
    CharacterController characterController;

    private bool canPlaySound = true;  // New flag to control sound playback

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        timer = decisionInterval;
    }
    private void Update()
    {
        MoveToMan();
    }
    void MoveToMan()
    {
        if (move)
        {
            // Calculate the direction to Anazua
            Vector3 directionToTarget = mainguy.transform.position - transform.position;
            directionToTarget.y = 0; // Ignore vertical movement to keep the character on the ground
            if (directionToTarget.magnitude > 9.5f) // Ensure we have a significant direction
            {
                // Rotate the character to face the target
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                moveVelocity = directionToTarget.normalized * runspeed;
                moveVelocity.y = 0f; // Apply gravity to movement
                characterController.Move(moveVelocity * Time.deltaTime);
                distancetoguy = directionToTarget.magnitude;
                anim.SetBool("running", true);
            }
            else
            {
                anim.SetBool("running", false);
                move = false;
                DecideAttack();
            }
        }
    }
    void Punch()
    {
        move = false;
        anim.SetTrigger("punch");
    }
    void Kick()
    {
        move = false;
        anim.SetTrigger("kick");
    }
    void DecideAttack()
    {
        int randomChoice = Random.Range(0, 2); // 0 for punch, 1 for kick

        if (randomChoice == 0)
        {
            Punch();
        }
        else
        {
            Kick();
        }
    }

    public void Playsound()
    {
        if (Kasauti && canPlaySound)
        {
            AudioManager.clip = PunchSound;
            AudioManager.Play();
            Kasauti = false;

            // Disable sound playback for a short period of time to avoid multiple triggers
            canPlaySound = false;
            Invoke("ResetSoundCooldown", 0.5f);  // Delay before the sound can play again
        }
    }

    private void ResetSoundCooldown()
    {
        canPlaySound = true;  // Allow the sound to be played again after a short delay
    }
}
