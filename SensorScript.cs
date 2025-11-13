using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public Animator anim;
    public bool IsHit = false;
    public GameObject Kaeffect; // Assuming this is a particle effect
    public GameObject theparent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Adui")
        {
            EnemyMover Y = other.GetComponentInParent<EnemyMover>();
            EnemyScript E = other.GetComponentInParent<EnemyScript>();
            ReactorScript Reaction = other.GetComponent<ReactorScript>();

            if (Y != null) { Y.Kasauti = true; }

            if (Y.Kasauti && !E.BeenHit)
            {
                Kaeffect.SetActive(true); // Activate the effect
                StartCoroutine(DisableEffectAfterDelay(Kaeffect, 0.5f)); // Wait before disabling

                Y.Playsound();
                E.BeenHit = true;
                Reaction.wika();
                E.Nimegongwa();
            }
        }
    }

    // Coroutine to disable the effect after a delay
    private IEnumerator DisableEffectAfterDelay(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (effect != null)
        {
            effect.SetActive(false);
        }
    }
}
