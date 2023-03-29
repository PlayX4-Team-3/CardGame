using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attack,
    Defense,
    Utility
}

public enum CardAttribute
{
    None = 0,
    Rock,
    Sissors,
    Paper
}

public class Cards : MonoBehaviour
{
    public int cc; // cost
    public CardType ct; // att def utilty
    public CardAttribute ca; // rock sissors paper

    public int cardPower; // att(2damage), def(2shield), util(+2 or -2)...
}
