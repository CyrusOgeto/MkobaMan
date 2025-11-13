using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkobaController : MonoBehaviour
{
    //Variable for the Animator
    public Animator anim;
    public int Health;
    EnemyScript enemyScript;
    public string[] moveQueue = new string[3];
    private float lastInputTime;
    public bool acting,shouldsnap;
    public int pressCount;
    private float KickCount = 1.0f;
    public MkobaMover mkobaMover;
    public MainLevelManager mainLevelManager;



    //Test variables
    public float AttackDistance;
    private int punchCount = 1;//punch variable
    private float lastPunchTime;//punch variable
    public float comboDelay = 1.0f; // Time allowed between punches to maintain the combo
    private string Nextvariable;

    void Start()
    {
        lastInputTime = 0f;
    }

    // Update is called once per frame
    void Update()
    { 
        DetectAttack();
    }
    void DetectAttack()
    {
        lastInputTime += Time.deltaTime;
        if (lastInputTime >= 1.0f)
        {
            SmoothInput();
        }
     
    }
    public void Punch()
    {
        if (punchCount > 3)
        {
            punchCount = 1;
        }
        Debug.Log(punchCount);
        anim.SetTrigger("Punch");
        anim.SetFloat("PunchAmount", punchCount);
        punchCount++;
    }
    public void SmoothInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveSettings(8.5f, "Punch");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            MoveSettings(8.5f, "Kick");
        }
    }
    private void MoveSettings(float pace,string fanya)
    {
        acting = true;
        AttackDistance = pace;
        Debug.Log("Nimebonyeza " + fanya);
        Nextvariable = fanya;
        lastInputTime = 0f;        
    }
    public void Move()
    {        
        anim.SetTrigger(Nextvariable);
        if(Nextvariable == "Punch")
        {
            Punch();
        }
        else if(Nextvariable =="Kick")
        {
            Kick();
        }
    }
    public void Kick()
    {
        
        if (KickCount > 3)
        {
            KickCount = 1;
        }
        Debug.Log(KickCount);
        anim.SetTrigger("Kick");
        anim.SetFloat("KickAmount", KickCount);
        KickCount++;
    }
}