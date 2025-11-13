using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SensoryScript : MonoBehaviour
{
    public bool HittingPart;
    public string TheBodypart;
    public string ThePartHit;
    public string MyparentName;
    public GameObject MyParentObject;
    public EnemyMover EnemyMover;
    void Start()
    { 
        EnemyMover = MyParentObject.GetComponent<EnemyMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
       if(other.gameObject.name == "Hitta")
        {
            var wapi = other.gameObject.GetComponent<SensoryScript>();
            Kugonga(wapi.TheBodypart);
        }
    }
    private void Kugonga(string position)
    {
        if (HittingPart)
        {
            ThePartHit = position;
            Debug.Log("Nimemgonga kwa " + ThePartHit);
        }
        else
        {
            Debug.Log("Nimegongwa " + TheBodypart);
            EnemyMover.Reaction(TheBodypart);
        }
    }
    
}
