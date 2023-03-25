using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, ICharacter
{
    public int PhysicalStrength;

    public int TotalCost { get; }

    public int RemainingCost { get; set; }

    public void Attack()
    {
        Debug.Log("Attacking");
    }
}
