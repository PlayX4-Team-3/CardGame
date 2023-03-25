using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void Start()
    {
        RemainingCost = TotalCost;
    }

    public void UseDeck(Card card)
    {
        if(RemainingCost >= card.Cost)
        {
            Debug.Log($"Using deck {deck.Name}");
            RemainingCost -= deck.Cost;
        }
        else
        {
            Debug.Log("코스트가 부족합니다");
        }
    }
}
