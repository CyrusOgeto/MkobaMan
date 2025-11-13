using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attacks/Simple Move")]
public class Move : ScriptableObject
{
    public AnimatorOverrideController AnimOverride;
    public int damage;
    public float readjust;
}
