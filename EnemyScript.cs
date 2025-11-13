using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health;
    public bool BeenHit = false;

    // Method to simulate when the enemy is hit
    public void Nimegongwa()
    {
        Debug.Log("Mkobaman Amenigonga");//The detector 
        StartCoroutine(ResetBeenHit()); // Start the coroutine to reset BeenHit after 0.5 seconds       
    }

    // Coroutine to reset BeenHit after a delay
    private IEnumerator ResetBeenHit()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        BeenHit = false;  // Reset BeenHit to false
        Debug.Log("BeenHit reset to false");//the detector
    }
}
