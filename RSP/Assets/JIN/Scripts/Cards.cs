using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None = 0,
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
    [SerializeField]
    public int cc;
    public CardType ct;
    public CardAttribute ca;

    public static implicit operator Cards(GameObject v)
    {
        throw new NotImplementedException();
    }
}
