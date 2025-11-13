using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public EnemyScript enemyScript;
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
    private bool hasReacted = false;
    private float reactTimer = 0f;
    bool moveDelayStarted = false;


    public AudioSource AudioManager;
    public AudioClip PunchSound;

    public bool canact;
    public int myhealth = 100;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        timer = decisionInterval;
    }
    private void Update()
    {
        if (move && !moveDelayStarted)
        {
            moveDelayStarted = true;
            StartCoroutine(WaitThenMove());
        }
        if (!move)
        {
            moveDelayStarted = false; // reset so it can trigger again next time
        }
        if(hasReacted && move)
        {
            MoveToMan();
        }
    }
    IEnumerator WaitThenMove()
    {
        anim.SetTrigger("Cocky");
        yield return new WaitForSeconds(3f);
        hasReacted = true;
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 720f * Time.deltaTime);
                moveVelocity = directionToTarget.normalized * runspeed;
                moveVelocity.y = 0f; // Apply gravity to movement
                characterController.Move(moveVelocity * Time.deltaTime);
                distancetoguy = directionToTarget.magnitude;
                anim.SetBool("running", true);
            }
            else
            {
                anim.SetBool("running", false);
                hasReacted = false;
                move = false;
                PerformRandomAttack();
            }
        }
    }

    public void Playsound()
    {

    }
    /// <summary>
    /// The Random Moves The Enemy Makes
    /// </summary>
    public void PerformRandomAttack()
    {
    // 0 = Punch, 1 = HookPunch (otherpunch), 2 = Kick, 3 = OtherKick (otherkick)
    int attackType = Random.Range(0, 4); // returns 0,1,2,3

    if (!canact) return;

    switch (attackType)
    {
        case 0:
            anim.SetTrigger("punch");
            Debug.Log("Nimefanya Crosspunch");
            break;
        case 1:
            anim.SetTrigger("otherpunch");
            Debug.Log("Nimefanya Hookpunch");
            break;
        case 2:
            anim.SetTrigger("kick");
            Debug.Log("Nimefanya Frontkick");
            break;
        case 3:
            anim.SetTrigger("otherkick");
            Debug.Log("Nimefanya RoundhouseKick");
            break;
    }

    canact = false;
    }
    

}
