using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComboLogic : MonoBehaviour
{
    private int tapCount = 0;
    private float lastTapTime;
    public List<Move> Punches;
    public List<Move> Kicks;
    public Move ComboPunch;
    public Animator anim;
    private bool incombo;


    public bool sensor;

    private const float comboResetTime = 0.6f; // Time window for combo
    private void Start()
    {
        anim.runtimeAnimatorController = Punches[0].AnimOverride;
    }
    public void Punch()
    {
        sensor = true;
        anim.SetTrigger("attack");
    }
    public void altpunch()
    {
        anim.runtimeAnimatorController = Punches[0].AnimOverride;
        anim.SetTrigger("attack");
    }
    public void altkick()
    {
        anim.runtimeAnimatorController = Kicks[1].AnimOverride;
        anim.SetTrigger("attack");
    }
    public void shield()
    {

    }
    public void Kick()
    {
        if (incombo) { return; }
        anim.runtimeAnimatorController = Kicks[0].AnimOverride;
        
        if (Time.time - lastTapTime > 0.5f)
        {
            tapCount = 1; // Reset tap count if time exceeds 0.5s
        }
        else
        {
            tapCount++;
        }

        Debug.Log("Amebofya mara " + tapCount.ToString());
        lastTapTime = Time.time;

        //CheckKickCombo();
    }
    public void CheckKickCombo()
    {
        if (tapCount >= 3)
        {
            incombo = true;
            anim.runtimeAnimatorController = ComboPunch.AnimOverride;
            anim.SetTrigger("attack");
            tapCount = 0;
            StartCoroutine(ResetAfterAnimation(2f));
            //StartCoroutine(ResetAfterAnimation(anim.GetCurrentAnimatorStateInfo(0).length));
            //tapCount = 0;
        }
        else
        {
            anim.SetTrigger("attack");
        }
    }
    public void CheckCombo()
    {
        if (tapCount >= 3)
        {
            incombo = true;
            anim.runtimeAnimatorController = ComboPunch.AnimOverride;
            anim.SetTrigger("attack");
            tapCount = 0;
            StartCoroutine(ResetAfterAnimation(2f));
            //StartCoroutine(ResetAfterAnimation(anim.GetCurrentAnimatorStateInfo(0).length));
            //tapCount = 0;
        }
        else
        {
            
            anim.SetTrigger("attack");
        }
    }
    //fix error of punches overlapping each other later
    private IEnumerator ResetAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay); // Or hardcode your combo anim length
        anim.runtimeAnimatorController = Punches[0].AnimOverride;
        incombo = false;
        Debug.Log("Combo finished, controller reset via coroutine.");
    }
}
