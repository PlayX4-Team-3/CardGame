using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbility : Singleton<CardAbility>
{
    public void UseCard(Card card)
    {
        string type = card.Type;
        string attribute = card.Attribute;

    }
}
