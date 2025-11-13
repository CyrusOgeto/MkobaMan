using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboLogic : MonoBehaviour
{
    public int tapcount;  // Tracks number of taps
    public List<Move> Punches;  // List of punch moves
    public List<Move> Kicks;    // List of kick moves
    public Move ComboPunch;     // Combo punch move
    public Move DoublePunch;    // Double tap punch move
    public Animator anim;
    public Animator SimpleAnim;
    public float lastClickedTime;  // Time of the last tap
    public float lastComboEnd;  // End time of last combo
    public int comboCounter;  // Counter for combo moves
    public float clickTimeDifference;  // Time between taps
    private bool isAttacking = false;  // Flag to track if an attack is in progress

    private void Update()
    {
        // Wait for the animation to end before allowing next input (if in attack state)
        if (isAttacking && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            isAttacking = false; // Reset attack flag
        }
    }

    // Method to handle punching (both single tap and combo detection)
    public void Punch()
    {
        clickTimeDifference = Time.time - lastClickedTime;
        lastClickedTime = Time.time;

        if (clickTimeDifference <= 0.3f) // If the tap is within 0.3 seconds
        {
            tapcount++; // Increment tap count for fast input detection
        }

        if (isAttacking) // Prevent input spam during attack animation
        {
            return;
        }

        // Handle Double Tap
        if (tapcount == 2) // Double Tap
        {
            Debug.Log("Double Tap detected!");
            anim.runtimeAnimatorController = DoublePunch.AnimOverride; // Use Double Punch animation
            anim.SetTrigger("attack");
            tapcount = 0; // Reset tap count after detecting double tap
            isAttacking = true; // Mark as attacking
        }
        // Handle Triple Tap
        else if (tapcount >= 3) // Triple Tap
        {
            Debug.Log("Triple Tap detected");
            anim.runtimeAnimatorController = ComboPunch.AnimOverride; // Use Combo Punch animation
            anim.SetTrigger("attack");
            tapcount = 0; // Reset tap count after detecting triple tap
            comboCounter = 0; // Reset combo counter for next moves
            isAttacking = true; // Mark as attacking
        }
        else
        {
            // Normal Punch Combo (Cycle through punches)
            Debug.Log("Punch");
            anim.runtimeAnimatorController = Punches[comboCounter].AnimOverride; // Use current punch animation
            anim.SetTrigger("attack");
            isAttacking = true; // Mark as attacking

            // Start combo progression
            StartCoroutine(WaitForCombo());
        }
    }

    // Coroutine that waits for the current attack animation to finish before progressing combo
    private IEnumerator WaitForCombo()
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f); // Wait until animation is 80% done
        comboCounter++; // Move to next punch in the combo
        if (comboCounter >= Punches.Count) // Reset combo counter if it's at the end
        {
            comboCounter = 0;
        }
        isAttacking = false; // Allow next input after animation ends
    }

    // Method to handle kick animations (just like punches)
    public void Kick()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            return;
        }
        if (Time.time - lastComboEnd > 0.2f && comboCounter <= Kicks.Count)
        {
            CancelInvoke("EndCombo");
            if (Time.time - lastClickedTime >= 0.2f)
            {
                anim.runtimeAnimatorController = Kicks[comboCounter].AnimOverride;
                anim.SetTrigger("attack");
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= Kicks.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    // Method to exit the attack state and reset combo if needed
    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            Invoke("EndCombo", 1); // End combo after attack animation finishes
        }
    }

    // Method to reset combo when it finishes
    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
