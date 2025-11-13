using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public Collider myCollider;
    public ComboLogic comboLogic;
    public float radius = 0.3f;
    public float range = 1.5f;
    public AudioSource audioSource;
    public AudioClip punchSound;

    private float lastPunchTime;
    public float punchCooldown = 0.15f;

    private void Start()
    {
        myCollider = GetComponentInParent<Collider>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

       
    }

    private void Update()
    {
        if (comboLogic.sensor)
        {
            Punch();
        }
    }

    public void Punch()
    {
        // Cooldown check
        if (Time.time - lastPunchTime < punchCooldown)
            return;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, range))
        {
            if (hit.collider == myCollider || hit.collider.transform.IsChildOf(transform))
                return;

            Debug.Log($"PUNCHED: {hit.collider.gameObject.name} with {gameObject.name}");

            // Play sound
            if (audioSource != null && punchSound != null)
            audioSource.PlayOneShot(punchSound);

            // Update cooldown
            lastPunchTime = Time.time;

            comboLogic.sensor = false;
        }
    }
}