using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, ICharacter
{
    public int Health { get; set; }
    public List<CardManager> DeckList { get; set; }
    public List<CardManager> HandList { get; set; }
    public int TotalCost { get; set; }
    public int RemainingCost { get; set; }
    public int MaxDuplicateCards { get; set; }

    public abstract void TurnOver(int damage);

    void Start()
    {
        Health = 100;
        DeckList = new List<CardManager>();
        HandList = new List<CardManager>();
        TotalCost = 0;
        RemainingCost = 0;
        MaxDuplicateCards = 3;

        CardManager cardmanager = GetComponent<CardManager>();
    }
}
