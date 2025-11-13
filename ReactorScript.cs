using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorScript : MonoBehaviour
{
    public string TheTrigger;
    public Animator anim;
     
    public void wika()
    {
        Debug.Log("NimeTrigger "+TheTrigger);//the detector
        anim.SetTrigger(TheTrigger);
    }
}
