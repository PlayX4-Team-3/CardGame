using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void Attack();
    int TotalCost { get; }
    int RemainingCost { get; set; }
}
