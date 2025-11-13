using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MkobaController : MonoBehaviour
{
    //Variable for the Animator
    public Animator anim;
    public int Health;
    EnemyScript enemyScript;
    private float lastInputTime;
    public bool acting,shouldsnap;
    //public MkobaMover mkobaMover;
    public MainLevelManager mainLevelManager;
    public bool InProximity;
    public ComboLogic comboLogic;

    public bool idle;
    public float idletimer = 0f;
    public float timer;

    void Start()
    {
        lastInputTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        if(idletimer > 5.0f)
        {
            idle = true;
            idletimer = 0f;
        }
    }
    public void CheckStatus()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("chilling"))
        {
            idletimer +=Time.deltaTime;
        }
        else
        {
            idletimer = 0f;
            idle = false;
        }
    }
}